using UnityEngine;


public class MusicPlayer : MonoBehaviour
{
    

    void Awake()
    {
        //An array here. Could be used in different ways
        int numMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;

        // If more than one destory object. Useful
        if (numMusicPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}


