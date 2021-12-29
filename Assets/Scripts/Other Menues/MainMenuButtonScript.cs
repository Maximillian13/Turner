// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuButtonScript : MonoBehaviour 
{
    private GameObject musicGO;
    private AudioSource music;

    // This has to be in update because the original audio source get destroyed
    void Update()
    {
        musicGO = GameObject.Find("Music");
        if (musicGO != null)
        {
            music = musicGO.GetComponent<AudioSource>();

            // This might not work right
            if (PlayerPrefs.GetFloat("MUSICVOLUME") != 0)
            {
                music.volume = PlayerPrefs.GetFloat("MUSICVOLUME");
            }
        }
    }

    // All buttons for the main menu
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LEVEL"));
    }

    public void LoadAnyLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
