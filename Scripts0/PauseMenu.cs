using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("GameMenus")]
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject StartMenuUI;
    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject LosingScreen;

    public static bool GameIsPaused = false;
    public static bool GameHasStarted = false;

    void Start ()
    {
        StartUpMenu();
    }

    private void StartUpMenu()
    {
        StartMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameHasStarted = false;
    }

    void Update ()
    {
        if (FindObjectOfType<FriendlyDamagingEnemy>() == null)
        {
            WinScreen.SetActive(true);
            Time.timeScale = 0;
        }
        if(FindObjectOfType<EnemyDamagingFriendly>() == null)
        {
            LosingScreen.SetActive(true);
            Time.timeScale = 0f;
        }
	if(Input.GetButtonDown("Cancel"))
        { 
            if (GameHasStarted == false)
            {
                StartMenuUI.SetActive(false);                          
            }
            if (GameIsPaused || GameHasStarted == false)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        StartMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameHasStarted = true; // Makes it true so it clicks off startMenu and doesn't go into Pause menu
    }

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitingGame()
    {
        Application.Quit();
    }
}
