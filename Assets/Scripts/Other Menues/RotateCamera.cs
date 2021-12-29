// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {

    private AudioSource music;

	// Use this for initialization
	void Start () 
    {
        // Find the music
        GameObject gOMusic = GameObject.Find("Music");
        if (gOMusic != null)
        {
            music = gOMusic.GetComponent("AudioSource") as AudioSource;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        // Moves the level and changes music
        if (music != null)
        {
            music.pitch = 1;
            music.panStereo = 0;
        }
        // Left arrow
        if (Input.GetButton("RoomLeft"))
        {
            if (music != null && PlayerPrefs.GetInt("DISTORT") == 0)
            {
                music.pitch = -1;
                music.panStereo = -.75f;
            }
            this.gameObject.transform.Rotate(0, 0, 1);
        }
        // Right Arrow
        else if (Input.GetButton("RoomRight"))
        {
            if (music != null && PlayerPrefs.GetInt("DISTORT") == 0)
            {
                music.pitch = -1;
                music.panStereo = .75f;
            }
            this.gameObject.transform.Rotate(0, 0, -1);
        }
	}
}
