using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour 
{
    AudioSource music;
	// Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(this.gameObject);
        music = this.GetComponent<AudioSource>();
        if (music != null)
        {
            music.volume = PlayerPrefs.GetFloat("MUSICVOLUME");
        }
	}

    void Update()
    {
        if (music != null)
        {
            music.volume = PlayerPrefs.GetFloat("MUSICVOLUME");
        }
    }
}
