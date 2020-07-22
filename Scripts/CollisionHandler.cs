using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Level Load Manager")]
    [Tooltip("Delay load on level in secs")] [SerializeField] float LevelLoadDelay = 2f;

    [Header("Special Visual Effects")]
    [Tooltip ("Death FX on player")] [SerializeField] GameObject deathFX;
    

    void OnTriggerEnter(Collider other)
    {
        StartDeath();
        
    }

    void StartDeath()
    {
        SendMessage("PlayerDeath");    //string
        
        deathFX.SetActive(true);      

        Invoke("ReloadScene", time: LevelLoadDelay); // String here as well, Method
        
    }


    void ReloadScene()                      // String method
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        
    }
}
