// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class RoomConstantRotation : MonoBehaviour
{
    public Transform room;
    public float roomSpeed;

    void FixedUpdate()
    {
        // If not paused
        if (Time.timeScale == 1)
        {
            room.Rotate(0, 0, roomSpeed);
        }
    }

}