using UnityEngine;
using UnityEngine.UI;

public class TowerFriendlyDamage : MonoBehaviour
{
    [Header("Health & Timer")]
    [SerializeField] float FTowerHP = 10;
    [SerializeField] float FTowerRespawnHP = 10;
    [SerializeField] float FriendlyreSpawnTimer = 1f;
    [SerializeField] float FriendlydeathTimer = 1f;

    [Header("Prefabs")]
    [SerializeField] ParticleSystem FTowerPrefabHit;
    [SerializeField] ParticleSystem TowerDeathParticle;

    [Header("Audio")]
    [SerializeField] AudioClip FriendlyTowerDisableClip;
    [SerializeField] AudioClip FriendlyTowerDmgCLip;

    [Header("Misc")]
    [SerializeField] Collider FTowerCollisionMesh;
    public Image FriendlyHealthBar;
    public bool isFriendlyTowerDead = false;

    void Start()
    {
        FTowerHP = FTowerRespawnHP;
    }
    void Update()
    {
        FriendlyDeathTowerRespawn();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (FTowerHP > .01 && isFriendlyTowerDead == false)
        {
            GetComponent<AudioSource>().PlayOneShot(FriendlyTowerDmgCLip);
            ProcessFriendlyTowerHit(1);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(FriendlyTowerDisableClip);
            DisableFriendlyTower();
        }
    }

    private void ProcessFriendlyTowerHit(float amount)
    {
        FTowerHP -= amount;
        FriendlyHealthBar.fillAmount = FTowerHP / FTowerRespawnHP;
        FTowerPrefabHit.Play();
    }

    private void DisableFriendlyTower()
    {
        isFriendlyTowerDead = true;
        TowerDeathParticle.Play();
        transform.GetChild(0).gameObject.SetActive(false);

    }

    private void FriendlyDeathTowerRespawn()
    {
        if (isFriendlyTowerDead == true)
        {
            FriendlydeathTimer -= Time.deltaTime;
            if (FriendlydeathTimer < 0f && isFriendlyTowerDead == true)
            {
                isFriendlyTowerDead = false;
                FTowerHP = FTowerRespawnHP;
                FriendlydeathTimer = FriendlyreSpawnTimer;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
