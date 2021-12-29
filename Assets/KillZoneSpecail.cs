using UnityEngine;
using System.Collections;

public class KillZoneSpecail : MonoBehaviour {

    // Resets the level
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Box")
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
