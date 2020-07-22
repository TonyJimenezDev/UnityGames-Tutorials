using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject[] Guns;

    // I've attached the enemy control to the camera. Becareful of a break here. Might want to seperate...

    [Header("Everything to do with X")]
    [Tooltip("In meters per sec")] [SerializeField] float xSpeed = 20f;
    [Tooltip("In meters")] [SerializeField] float xRange = 4.78f;

    [Header("Everything to do with Y")]
    [Tooltip("In meters per sec")] [SerializeField] float ySpeed = 20f;
    [Tooltip("In meters")] [SerializeField] float yNegRange = 4.78f;
    [Tooltip("In meters")] [SerializeField] float yPostiveRange = 4.78f;

    [Header("Screen Position")]
    [SerializeField] float positionPitchFact = -7f; // Controls ship aiming on the y axis
    [SerializeField] float positionYawFact = 8f;    // Controls Ship aiming on the x axis

    [Header("Control throw")]
    [SerializeField] float controlPitchFact = -20f; // Controls the nose on the y axis 
    [SerializeField] float controlYawFact = 20f;    // Controls Ship nose on the x axis
    //[SerializeField] float controlRollFact = 5f;    // controls the rolls of the ship, We used a different roll

    [SerializeField] GameObject heroShip;

    float xThrow;
    float yThrow;

    bool isControlWorking = true;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if (isControlWorking)
        {
            ShipMotion();
            RotateShip();
            Firing();
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        if (Input.GetButton("Submit"))
        {
            SceneManager.LoadScene(1);
        }
    }

    void PlayerDeath() // This is a string name. Manually rename it
    {
        isControlWorking = false;
        Invoke("HeroDestroyed", 1.1f);
    }

    void HeroDestroyed()
    {
        heroShip.SetActive(false);
    }

    void RotateShip()
    {
        //Had to readjust default rotation, due to it leaning to one side. Code line above
        float pitchDueToPosition = transform.localPosition.y * positionPitchFact + -6.5f;
        

        float pitchDueToControl = yThrow * controlPitchFact;
        float pitch = pitchDueToPosition + pitchDueToControl;
        
        float yawDueToPosition = transform.localPosition.x * positionYawFact;
        // float roll = xThrow * rollDueToControlThrow - add this and top to get a more static turn/roll

        //Enjoyed this rolling and turning instead
        float yawDuetoControl = xThrow * controlYawFact;                    
        float yaw = yawDueToPosition + yawDuetoControl;

        //float rollDueToPosition = transform.localPosition.z * positionRollFact;

        float roll = pitch + yaw; // This controls the roll

        //float roll = rollDueToPosition + rollDueToControl;                                            

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    // Goe from one side to the other with the ship. It gets clamped so it doesn't leave screen
    void ShipMotion()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;

        float rawPos = transform.localPosition.x + xOffset;
        float ClampXPos = Mathf.Clamp(rawPos, -xRange, xRange);

        transform.localPosition = new Vector3(ClampXPos, transform.localPosition.y, transform.localPosition.z);
    

    // Go up or down with the ship. It gets clamped so it doesn't leave screen
    
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = ySpeed * yThrow * Time.deltaTime;

        float yRawPos = transform.localPosition.y + yOffset;
        float clampYPos = Mathf.Clamp(yRawPos, -yNegRange, yPostiveRange);

        transform.localPosition = new Vector3(ClampXPos, clampYPos, transform.localPosition.z);

    }

    //Might want to make this into a lazer if you will be leaving it as a on and off
    void Firing()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            MakeGunsActive(true);
        }
        else
        {
            MakeGunsActive(false);
        }
    }

    private void MakeGunsActive(bool isActive)
    {
        foreach (GameObject gun in Guns)
        {
            var psEmission = gun.GetComponent<ParticleSystem>().emission; // Might effect death effects. Grabs all PS
            psEmission.enabled = isActive;          
        };
    }
}
