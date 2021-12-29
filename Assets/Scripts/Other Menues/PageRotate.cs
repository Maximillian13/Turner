// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PageRotate : MonoBehaviour
{
    // Fields
    public Transform page;
    private AudioSource music;

    // Finds the music
    void Start()
    {
        GameObject gOMusic = GameObject.Find("Music");
        if (gOMusic != null)
        {
            music = gOMusic.GetComponent("AudioSource") as AudioSource;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Controls rotation and changing music
        if (music != null)
        {
            music.pitch = 1;
        }
        // Left arrow
        if (Input.GetButton("RoomLeft") && PlayerPrefs.GetInt("DISTORT") == 0)
        {
            if (music != null)
            {
                music.pitch = -1;
            }
            page.Rotate(0, 0, 2);
        }
        //Right arrow
        else if (Input.GetButton("RoomRight") && PlayerPrefs.GetInt("DISTORT") == 0)
        {
            if (music != null)
            {
                music.pitch = -1;
            }
            page.Rotate(0, 0, -2);
        }

        // Set it back to the correct rotation
        if(PageButtonScripts.changed == true)
        {
            page.rotation = new Quaternion(0, 0, 0, page.rotation.w);
            PageButtonScripts.changed = false;
        }
    }
}
