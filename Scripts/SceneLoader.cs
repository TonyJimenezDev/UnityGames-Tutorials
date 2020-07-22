using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 5f;

    void Start()
    {
        

    }


    void Update()
    {
        Invoke("Menu", time: levelLoadDelay);
    }

    void Menu()
    {
        if (Input.GetButton("Fire1"))
        {
            SceneManager.LoadScene(1);
             
        }
    }
}
