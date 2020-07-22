using UnityEngine;
using UnityEngine.UI;

public class EnemyTowerDamage : MonoBehaviour
{
    [Header("Health & Respawn")]
    [SerializeField] float EnemyStartHealth = 10;
    [SerializeField] float CurrentHealth = 10;
    [SerializeField] float reSpawnTimer = 1f;
    [SerializeField] float deathTimer = 1f;

    [Header("Audio")]
    [SerializeField] AudioClip EnemyTowerDisableClip;
    [SerializeField] AudioClip EnemyTowerDmgCLip;

    [Header("Misc")]
    [SerializeField] ParticleSystem TowerDeathParticle;
    [SerializeField] ParticleSystem ETowerPrefabHit;
    [SerializeField] Collider ETowerCollisionMesh;

    public bool isTowerDead = false;
    public Image healthBar;

    private void Start()
    {
        CurrentHealth = EnemyStartHealth; 
    }

    void Update()
    {
        DeathTowerRespawn();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (CurrentHealth >= .01 && isTowerDead == false)
        {
            GetComponent<AudioSource>().PlayOneShot(EnemyTowerDmgCLip);
            ProcessETowerHit(1);
        } 
        else
        {
            GetComponent<AudioSource>().PlayOneShot(EnemyTowerDisableClip);
            DisableETower();       
        }
    }

    public void ProcessETowerHit(float amount)
    {
        CurrentHealth -= amount;
        healthBar.fillAmount = CurrentHealth / EnemyStartHealth;
        ETowerPrefabHit.Play();
    }

    private void DisableETower()
    {
        isTowerDead = true;
        TowerDeathParticle.Play();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void DeathTowerRespawn()
    {
        if (isTowerDead == true)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0f && isTowerDead == true)
            {
                isTowerDead = false;
                CurrentHealth = EnemyStartHealth;
                deathTimer = reSpawnTimer;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
