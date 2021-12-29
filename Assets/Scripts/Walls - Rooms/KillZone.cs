// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour 
{
    // Resets the level
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerControlsStart.killPlayer = true;
            PlayerControls.killPlayer = true;
            PlayerControlsDoubleJump.killPlayer = true;
            PlayerControlsCling.killPlayer = true;
            PlayerControlsBlink.killPlayer = true;
            PlayerControlsDisoriented.killPlayer = true;
            PlayerControlsEnd.killPlayer = true;
        }
    }
}
