using UnityEngine;
using System.Collections;

public class VolumeControl : MonoBehaviour 
{
    AudioSource music;
	
	void Update ()
    {
        music = this.GetComponent<AudioSource>();
        if (music != null)
        {
            music.volume = PlayerPrefs.GetFloat("MUSICVOLUME");
        }
	}
}
