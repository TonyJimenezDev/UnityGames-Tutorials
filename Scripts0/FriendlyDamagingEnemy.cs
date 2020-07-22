using UnityEngine;
using UnityEngine.UI;

public class FriendlyDamagingEnemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float enemyhealth = 20;
    [SerializeField] float EnemyStartHeath = 20;
    [SerializeField] float enemySmallDamageHealth = 1;
    [SerializeField] float enemyBigDamageHealth = 5;

    [Header("Audio")]
    [SerializeField] AudioClip SmallEnemyExplosionSFX;
    [SerializeField] AudioClip BigEnemyExplosionSFX;
    [SerializeField] AudioClip EnemyBaseExplosionSFX;

    [Header("Misc")]
    [SerializeField] FriendlyMovement friendlyprefab1; // Check to see if this made a difference
    [SerializeField] FriendlyMovement friendlyprefab2; // Check to see if this made a difference  
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] Text healthText;

    public Image EnemyBaseHealthBar;
    public GameObject[] Enemybase;

    private void Start()
    {
        healthText.text = enemyhealth.ToString();
        enemyhealth = EnemyStartHeath;
    }

    private void WinScreen()
    {
        var EnemyDeathExplosion = Instantiate(explosionFX, transform.position, Quaternion.identity);
        float destroyDelayforExplosion = explosionFX.main.duration;
        EnemyDeathExplosion.Play();
        Time.timeScale = .5f;
        GetComponent<AudioSource>().PlayOneShot(EnemyBaseExplosionSFX);

        for (int i = Enemybase.Length - 1; i >= 0; i--)         // This could also be done forward but best practice it backwards for inventory deleting. 
        {                
            Destroy(Enemybase[i], destroyDelayforExplosion);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FriendlySmall")
        {
            GetComponent<AudioSource>().PlayOneShot(SmallEnemyExplosionSFX);
            enemyhealth = enemyhealth - enemySmallDamageHealth;
            healthText.text = enemyhealth.ToString();   
        }
        if (other.tag == "FriendlyBig")
        {
            GetComponent<AudioSource>().PlayOneShot(BigEnemyExplosionSFX);
            enemyhealth = enemyhealth - enemyBigDamageHealth;
            healthText.text = enemyhealth.ToString();  
        }

        EnemyBaseHealthBar.fillAmount = enemyhealth / EnemyStartHeath;
        if (enemyhealth <= 0.01f)
            {
                WinScreen();
                healthText.text = "0";
            }
        }
    }
