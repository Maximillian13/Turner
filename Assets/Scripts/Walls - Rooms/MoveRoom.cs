// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class MoveRoom : MonoBehaviour 
{
    public Transform room;
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
    }
	
	void FixedUpdate () 
    {
        // If not paused
        if (Time.timeScale == 1)
        {
            // Sets the Room rotate speed
            distance = Vector3.Distance(player.transform.position, new Vector3(0, 0, 0));
            if (distance > 12)
            {
                roomSpeed = .7f * 1.4f;

            }
            else if (distance > 6)
            {
                roomSpeed = .8f * 1.4f;

            }
            else
            {
                roomSpeed = 1 * 1.4f;

            }


            // Left Arrow
            if (Input.GetButton("RoomLeft"))
            {
                room.Rotate(0, 0, roomSpeed);
            }
        
            // Right Arrow
            else if (Input.GetButton("RoomRight"))
            {
                room.Rotate(0, 0, -roomSpeed);
            }
        }
	}

}