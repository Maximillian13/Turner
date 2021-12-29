using UnityEngine;
using System.Collections;

public class MoveRoomSpecail : MonoBehaviour {

    public Transform room;
    public float roomSpeed;

    void FixedUpdate()
    {
        // If not paused
        if (Time.timeScale == 1)
        {
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
