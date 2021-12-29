using UnityEngine;
using System.Collections;

public class ClingKill : MonoBehaviour 
{
    // Resets the level
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Kill")
        {
            PlayerControlsCling.killPlayer = true;
        }
    }
}
