// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour 
{
    private GameObject musicScript;
    private GameObject[] songs;
    //private AudioSource[] music;

	// Use this for initialization
	void Start () 
    {
        // Let it go through the levels
        musicScript = GameObject.Find("Music Script");
        if (musicScript != this.gameObject)
        {
            Destroy(musicScript);
        }
        DontDestroyOnLoad(this.gameObject);
        songs = GameObject.FindGameObjectsWithTag("Music");
        if(songs.Length > 1)
        {
            for (int x = 1; x < songs.Length; x++)
            {
                if (songs[x].name == "CutSceneMusic" || songs[x].name == "LastSong")
                {
                    Destroy(songs[0]);
                }
                else
                {
                    Destroy(songs[x]);
                }
            }
        }
    }
	
}
