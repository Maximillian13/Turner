// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class GroundTester : MonoBehaviour 
{
    // Fields
    private bool slantLeft;
    private bool slantRight;
    private float direction;

    private bool leftTest;
    private bool rightTest;
    private bool boxRight;
    private bool boxLeft;

    void Start()
    {
        PlayerControlsStart.direction = 0;
        PlayerControls.direction = 0;
        PlayerControlsDoubleJump.direction = 0;
        PlayerControlsCling.direction = 0;
    }

    void Update()
    {
        // For slant
        Debug.DrawLine(new Vector2(this.transform.position.x - .075f, this.transform.position.y), new Vector2(this.transform.position.x - .075f, this.transform.position.y - .65f), Color.yellow);
        slantLeft = Physics2D.Linecast(new Vector2(this.transform.position.x - .075f, this.transform.position.y), new Vector2(this.transform.position.x - .075f, this.transform.position.y - .65f), 1 << LayerMask.NameToLayer("Wall"));

        Debug.DrawLine(new Vector2(this.transform.position.x + .075f, this.transform.position.y), new Vector2(this.transform.position.x + .075f, this.transform.position.y - .65f), Color.yellow);
        slantRight = Physics2D.Linecast(new Vector2(this.transform.position.x + .075f, this.transform.position.y), new Vector2(this.transform.position.x + .075f, this.transform.position.y - .65f), 1 << LayerMask.NameToLayer("Wall"));

        if(PlayerControlsStart.direction != 0)
        {
            direction = PlayerControlsStart.direction;
        }
        else if(PlayerControls.direction != 0)
        {
            direction = PlayerControls.direction;
        }
        else if (PlayerControlsDoubleJump.direction != 0)
        {
            direction = PlayerControlsDoubleJump.direction;
        }
        else if (PlayerControlsCling.direction != 0)
        {
            direction = PlayerControlsCling.direction;
        }
        else if (PlayerControlsBlink.direction != 0)
        {
            direction = PlayerControlsBlink.direction;
        }

        if(slantLeft == false && slantRight == true && direction == -1)
        {
            PlayerControlsStart.slantTwo = true;
            PlayerControlsStart.slant = false;
            PlayerControls.slantTwo = true;
            PlayerControls.slant = false;
            PlayerControlsDoubleJump.slantTwo = true;
            PlayerControlsDoubleJump.slant = false;
            PlayerControlsCling.slantTwo = true;
            PlayerControlsCling.slant = false;
            PlayerControlsBlink.slantTwo = true;
            PlayerControlsBlink.slant = false;
        }
        else if (slantLeft == false && slantRight == true && direction == 1)
        {
            PlayerControlsStart.slant = true;
            PlayerControlsStart.slantTwo = false;
            PlayerControls.slant = true;
            PlayerControls.slantTwo = false;
            PlayerControlsDoubleJump.slant = true;
            PlayerControlsDoubleJump.slantTwo = false;
            PlayerControlsCling.slant = true;
            PlayerControlsCling.slantTwo = false;
            PlayerControlsBlink.slant = true;
            PlayerControlsBlink.slantTwo = false;
        }
        else if (slantLeft == true && slantRight == false && direction == -1)
        {
            PlayerControlsStart.slant = true;
            PlayerControlsStart.slantTwo = false;
            PlayerControls.slant = true;
            PlayerControls.slantTwo = false;
            PlayerControlsDoubleJump.slant = true;
            PlayerControlsDoubleJump.slantTwo = false;
            PlayerControlsCling.slant = true;
            PlayerControlsCling.slantTwo = false;
            PlayerControlsBlink.slant = true;
            PlayerControlsBlink.slantTwo = false;
        }
        else if (slantLeft == true && slantRight == false && direction == 1)
        {
            PlayerControlsStart.slantTwo = true;
            PlayerControlsStart.slant = false;
            PlayerControls.slantTwo = true;
            PlayerControls.slant = false;
            PlayerControlsDoubleJump.slantTwo = true;
            PlayerControlsDoubleJump.slant = false;
            PlayerControlsCling.slantTwo = true;
            PlayerControlsCling.slant = false;
            PlayerControlsBlink.slantTwo = true;
            PlayerControlsBlink.slant = false;
        }
        else
        {
            PlayerControlsStart.slant = false;
            PlayerControlsStart.slantTwo = false;
            PlayerControls.slant = false;
            PlayerControls.slantTwo = false;
            PlayerControlsDoubleJump.slant = false;
            PlayerControlsDoubleJump.slantTwo = false;
            PlayerControlsCling.slant = false;
            PlayerControlsCling.slantTwo = false;
            PlayerControlsBlink.slant = false;
            PlayerControlsBlink.slantTwo = false;
        }

        // Raycast for the Left side
        Debug.DrawLine(new Vector2(this.transform.position.x - .11f, this.transform.position.y), new Vector2(this.transform.position.x - .11f, this.transform.position.y - .75f), Color.red);
        leftTest = Physics2D.Linecast(new Vector2(this.transform.position.x - .11f, this.transform.position.y), new Vector2(this.transform.position.x - .11f, this.transform.position.y - .75f), 1 << LayerMask.NameToLayer("Wall"));

        // Raycast for the Right side
        Debug.DrawLine(new Vector2(this.transform.position.x + .11f, this.transform.position.y), new Vector2(this.transform.position.x + .11f, this.transform.position.y - .75f), Color.red);
        rightTest = Physics2D.Linecast(new Vector2(this.transform.position.x + .11f, this.transform.position.y), new Vector2(this.transform.position.x + .11f, this.transform.position.y - .75f), 1 << LayerMask.NameToLayer("Wall"));

        // Raycast for the Left side
        boxLeft = Physics2D.Linecast(new Vector2(this.transform.position.x - .11f, this.transform.position.y), new Vector2(this.transform.position.x - .11f, this.transform.position.y - .75f), 1 << LayerMask.NameToLayer("Box"));

        // Raycast for the Right side
        boxRight = Physics2D.Linecast(new Vector2(this.transform.position.x + .11f, this.transform.position.y), new Vector2(this.transform.position.x + .11f, this.transform.position.y - .75f), 1 << LayerMask.NameToLayer("Box"));

        if (leftTest || rightTest || boxLeft || boxRight)
        {
            // Sets all of the turners to be right
            PlayerControlsStart.grounded = true;
            PlayerControls.grounded = true;
            PlayerControlsDoubleJump.grounded = true;
            PlayerControlsCling.grounded = true;
            PlayerControlsBlink.grounded = true;
            PlayerControlsDisoriented.grounded = true;
            PlayerControlsEnd.grounded = true;
            PlayerControlsTrue.grounded = true;
        }
        else
        {
            PlayerControlsStart.grounded = false;
            PlayerControls.grounded = false;
            PlayerControlsDoubleJump.grounded = false;
            PlayerControlsCling.grounded = false;
            PlayerControlsBlink.grounded = false;
            PlayerControlsDisoriented.grounded = false;
            PlayerControlsEnd.grounded = false;
            PlayerControlsTrue.grounded = false;
        }

        PlayerControls.groundedLeft = leftTest;
        PlayerControls.groundedRight = rightTest;
        PlayerControlsDoubleJump.groundedLeft = leftTest;
        PlayerControlsDoubleJump.groundedRight = rightTest;
        PlayerControlsCling.groundedLeft = leftTest;
        PlayerControlsCling.groundedRight = rightTest;
        PlayerControlsBlink.groundedLeft = leftTest;
        PlayerControlsBlink.groundedRight = rightTest;
    }
}
