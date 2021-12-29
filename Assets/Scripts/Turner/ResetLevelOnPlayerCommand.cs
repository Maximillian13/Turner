// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetLevelOnPlayerCommand : MonoBehaviour 
{
    // Fields and Consts
    private float reset;
    private const float TimeToHold = .2f;

    // Set the timer back to 0
    void Start()
    {
        reset = 0;
    }

	void Update () 
    {
        if(Time.timeScale == 0)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

        // If the "R" key is being held start counting up, else set the time back to 0
        if (Input.GetButton("ResetLevel"))
        {
            reset += Time.deltaTime;
        }
        else
        {
            reset = 0;
        }

        // If the counter reaches the TimeToHold reset level
        if (reset > TimeToHold)
        {
            PlayerControlsStart.killPlayer = true;
            PlayerControls.killPlayer = true;
            PlayerControlsDoubleJump.killPlayer = true;
            PlayerControlsCling.killPlayer = true;
            PlayerControlsDisoriented.killPlayer = true;
            PlayerControlsBlink.killPlayer = true;
        }
	}
}
