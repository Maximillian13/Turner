using UnityEngine;
using System.Collections;

public class MusicBegining : MonoBehaviour 
{
    private AudioSource music;
	// Use this for initialization
	void Start () 
    {
        music = this.GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("MUSICGOOD") == 0)
        {
            music.volume = .2f;
            PlayerPrefs.SetFloat("MUSICVOLUME", .2f);
        }
        else
        {
            music.volume = PlayerPrefs.GetFloat("MUSICVOLUME");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
