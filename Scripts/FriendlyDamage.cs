using UnityEngine;

public class FriendlyDamage : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int FriendlyHitPoints = 10;

    [Header("Audio")]
    [SerializeField] AudioClip FriendlyDamageAudio;

    [Header("Prefab")]
    [SerializeField] ParticleSystem FriendlyHitParticlePrefab;
    [SerializeField] ParticleSystem FriendlydeathFx;
    [SerializeField] Collider collisionMesh;

    private void OnParticleCollision(GameObject other)
    {
        if (FriendlyHitPoints >= 1)
        {
            FriendlyHitParticlePrefab.Stop();
            ProcessHit();
            GetComponent<AudioSource>().PlayOneShot(FriendlyDamageAudio);
        }
        else
        {
            KillFriendly();
        }
    }

    private void ProcessHit()
    {
        FriendlyHitPoints = FriendlyHitPoints - 1;
        FriendlyHitParticlePrefab.Play();
    }

    private void KillFriendly()
    {
        var playDeathPart = Instantiate(FriendlydeathFx, transform.position, Quaternion.identity);
        float destroyDelay = playDeathPart.main.duration;

        playDeathPart.Play();
        Destroy(playDeathPart.gameObject, destroyDelay);
        Destroy(gameObject);
    }
}
