using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathFx;
    [SerializeField] FriendlyMovement prefab; // Spawns a big friendly over
    [SerializeField] Collider collisionMesh;

    [Header("Audio")]
    [SerializeField] AudioClip EnemyDamageAudio;

    [Header("Misc")]
    [SerializeField] int hitPoints = 10;

    private EnemySpawner enemySpawner;
    public int scoreValue;

    private void Start()
    {
        GameObject enemies = GameObject.FindWithTag("EnemiesTag");
        if (enemies != null)
        {
            enemySpawner = enemies.GetComponent<EnemySpawner>();
        }
        if (enemySpawner == null)
        {
            Debug.Log("Cannot find 'EnemyDamage' script");
        }
    }

    private void OnParticleCollision(GameObject other)
    {      

         if (hitPoints >= 1)
          {
            hitParticlePrefab.Stop();
              ProcessHit();
            GetComponent<AudioSource>().PlayOneShot(EnemyDamageAudio);
        }
          else
          {
              KillEnemy();
          }
    }

    private void BonusPoints()
    {
       Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
    }
    //HP of Bot
    private void ProcessHit()
    {
       hitPoints = hitPoints - 1;
       hitParticlePrefab.Play();
    }

    private void KillEnemy()
        {
        var playDeathPart = Instantiate(deathFx, transform.position, Quaternion.identity);
        float destroyDelay = playDeathPart.main.duration;

        if (tag == "EnemyBig")
        {
            BonusPoints(); // Controls the bonus Gameobject into friendly field
                          
            enemySpawner.AddScore(scoreValue);
            playDeathPart.Play();
            Destroy(playDeathPart.gameObject, destroyDelay);
            Destroy(gameObject);
        }
        else
        {
            // I have the Particle destroying itself through the Editor.
            playDeathPart.Play();      
            enemySpawner.AddScore(scoreValue);
            Destroy(playDeathPart.gameObject, destroyDelay);
            Destroy(gameObject);
        }       
    }    
}
