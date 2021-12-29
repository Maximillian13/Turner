// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class MoveRoomDisoriented : MonoBehaviour
{
    public Transform room;
    private AudioSource music;
    private GameObject player;
    private float roomSpeed;
    private float distance;


    void Start()
    {
        // Set the default
        roomSpeed = 1;
        distance = 0;

        // Find objects
        player = GameObject.FindWithTag("Player");

        // Find the music
        GameObject gOMusic = GameObject.Find("Music");
        if (gOMusic != null)
        {
            music = gOMusic.GetComponent("AudioSource") as AudioSource;
        }

    }

    void FixedUpdate()
    {
        // If not paused
        if (Time.timeScale == 1)
        {
            // Sets the Room rotate speed
            distance = Vector3.Distance(player.transform.position, new Vector3(0, 0, 0));
            if (distance > 12)
            {
                roomSpeed = Random.Range(-1, 2.5f) * 1.4f;
            }
            else if (distance > 6)
            {
                roomSpeed = Random.Range(-1, 2.5f) * 1.4f;
            }
            else
            {
                roomSpeed = Random.Range(-1, 2.5f) * 1.4f;
            }

            // Moves the level and changes music
            if (music != null)
            {
                music.pitch = 1;
                //music.panStereo = 0;
            }

            // Left Arrow
            if (Input.GetButton("RoomLeft"))
            {
                if (music != null && PlayerPrefs.GetInt("DISTORT") == 0)
                {
                    music.pitch = Random.Range(-1.1f, -.5f);
                    music.panStereo = Random.Range(-.9f, -.5f);
                }
                room.Rotate(0, 0, roomSpeed);
                //cam.orthographicSize = (Mathf.Abs(a.transform.position.y) + Mathf.Abs(a.transform.position.y)) / 2 + 2;
            }

            // Right Arrow
            else if (Input.GetButton("RoomRight"))
            {
                if (music != null && PlayerPrefs.GetInt("DISTORT") == 0)
                {
                    music.pitch = Random.Range(-1.1f, -.5f);
                    music.panStereo = Random.Range(.5f, .9f);
                }
                room.Rotate(0, 0, -roomSpeed);
                //cam.orthographicSize = (Mathf.Abs(a.transform.position.y) + Mathf.Abs(a.transform.position.y)) / 2;
            }
        }
    }

}