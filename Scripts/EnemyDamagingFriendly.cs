using UnityEngine;
using UnityEngine.UI;

public class EnemyDamagingFriendly : MonoBehaviour
{
    [Header("Health & DMG")]
    [SerializeField] float friendlyhealth = 50;
    [SerializeField] float friendlyStartHealth = 50;
    [SerializeField] float friendlySmallDamageHealth = 1;
    [SerializeField] float friendlyBigDamageHealth = 5;

    [Header("Audio")]
    [SerializeField] AudioClip SmallFriendlyExplosionSFX;
    [SerializeField] AudioClip BigFriendlyExplosionSFX;
    [SerializeField] AudioClip FriendlyBaseExplosionSFX;

    [Header("Misc")]
    [SerializeField] Collider Enemyprefab1;
    [SerializeField] Collider Enemyprefab2;
    [SerializeField] ParticleSystem LosingExplosion;

    public Text healthText;
    public GameObject[] friendlyBase;
    public Image FriendlyBaseHealthBar;

    private void Start()
    {
        healthText.text = friendlyhealth.ToString();
        friendlyhealth = friendlyStartHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemySmall") //String Tag
        {
            friendlyhealth = friendlyhealth - friendlySmallDamageHealth;
            healthText.text = friendlyhealth.ToString();
            GetComponent<AudioSource>().PlayOneShot(SmallFriendlyExplosionSFX);
        }
        if(other.tag == "EnemyBig") // String Tag
        {
            friendlyhealth = friendlyhealth - friendlyBigDamageHealth;
            healthText.text = friendlyhealth.ToString();
            GetComponent<AudioSource>().PlayOneShot(BigFriendlyExplosionSFX);
        }
        FriendlyBaseHealthBar.fillAmount = friendlyhealth / friendlyStartHealth;
        
        if (friendlyhealth <= 0)
        { 
            LosingScreen();
            healthText.text = "0";
        }
    }

    private void LosingScreen()
    {
        var enemyDefeatsYou = Instantiate(LosingExplosion, transform.position, Quaternion.identity);
        float defeatsYouDelay = LosingExplosion.main.duration;
        enemyDefeatsYou.Play();
        GetComponent<AudioSource>().PlayOneShot(FriendlyBaseExplosionSFX);
        Time.timeScale = .5f;

        for(int i = friendlyBase.Length - 1; i >= 0; i--)
        {
            Destroy(friendlyBase[i], defeatsYouDelay);  
        } 
    }
}
