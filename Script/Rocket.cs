using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    [SerializeField] float sideThurst = 200f;
    [SerializeField] float mainThurst = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathOfRocket;
    [SerializeField] AudioClip winnerChickenDinner;
    

    [SerializeField] ParticleSystem rocketFirePart;
    [SerializeField] ParticleSystem theWinPart;
    [SerializeField] ParticleSystem deathPart;

    enum State { Alive, Dying, Transcending, FinishedGame }
    State state = State.Alive;

    Rigidbody rigidBody;
    AudioSource audioSource;    
    

    // When game starts do this
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
        
    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            Thurst();
            RotateRocket();
            //DebugGame();                            // REMOVE WHEN GAME IS FINISHED
        } 
	}

    // If collision run. if dead return
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;                                         // Will not run collision. When dead. Saving performance
        }
        ;
        switch (collision.gameObject.tag)
        {
            case "Friendly":               
                break;
            case "StageComplete":
                state = State.Transcending;
                Winner();                                            
                break;                
            default:
                state = State.Dying;
                DeathStage();
                break;
        }
    }


    //Deathstage state w/ audio -->GameOver
    private void DeathStage()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(deathOfRocket);
        rocketFirePart.Stop();
        deathPart.Play();
        
        Invoke("GameOver", time: levelLoadDelay);
    }

    //Win state w/ audio --> LoadNextScene
    private void Winner()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(winnerChickenDinner);
        rocketFirePart.Stop();
        theWinPart.Play();
        Invoke("LoadNextScene", time: levelLoadDelay); 
    }


    
    //Restart at lvl 0
    private void GameOver()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (state == State.Dying)
        {
            SceneManager.LoadScene(scene.name);            
        } 

        if(state == State.FinishedGame) { 

            SceneManager.LoadScene(0);
        }
    }

    // Loads next level from current level
    private void LoadNextScene()
    {      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

   
    // Fly with fire
    private void Thurst()
    {        
        float thurstOnFrame = mainThurst * Time.deltaTime;
        if (CrossPlatformInputManager.GetAxis("Jump") != 0f)                                //Thurst when space bar at any moment
        {
            rigidBody.AddRelativeForce(Vector3.forward * thurstOnFrame);
            rocketFirePart.Play();
        }
        AudioThrusters();                                                   // Rocket sound
    }

    //rotates and gives us manuel control
    private void RotateRocket()
    {
        rigidBody.angularVelocity = Vector3.zero;

        float rotationOnFrame = sideThurst * Time.deltaTime; 

        if (CrossPlatformInputManager.GetAxis("Horizontal") < 0f)
        {            
            transform.Rotate(Vector3.down * rotationOnFrame);           // Turn Left  

        }
        else if (CrossPlatformInputManager.GetAxis("Horizontal") > 0f)
        {   
            transform.Rotate(Vector3.up * rotationOnFrame);             // Turn Right
        }

    }

    // Gives sound when space bar/thurst is performed/ stops firejets
    private void AudioThrusters() 
    {

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            audioSource.PlayOneShot(mainEngine);
        }
        else if (CrossPlatformInputManager.GetAxis("Jump") == 0f)
        {
            audioSource.Stop();
            rocketFirePart.Stop();
        }
    }

    //Use to help me get through levels quickly. K = skips lvls, C = toggles collider
    void DebugGame()
    {
        if(Input.GetKey(KeyCode.K))
        {
            LoadNextScene();
        }
        if (Input.GetKey(KeyCode.C))
        {
            GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;
        }
    }
}



