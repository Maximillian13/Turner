// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class WallTesterRight : MonoBehaviour 
{
    private bool top;
    private bool bot;
    private bool mid;

    // Checks to see if Turner is up against the right wall
    void Update()
    {
        // Top Right side
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y + .35f), new Vector2(this.transform.position.x + .3f, this.transform.position.y + .35f), Color.red);
        // Bot Right side
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y - .35f), new Vector2(this.transform.position.x + .3f, this.transform.position.y - .35f), Color.red);
        // Mid Left Side
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + .3f, this.transform.position.y), Color.red);

        // Sets all the stuff right
        top = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y + .35f), new Vector2(this.transform.position.x + .3f, this.transform.position.y + .35f), 1 << LayerMask.NameToLayer("Wall"));
        bot = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y - .35f), new Vector2(this.transform.position.x + .3f, this.transform.position.y - .35f), 1 << LayerMask.NameToLayer("Wall"));
        mid = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + .3f, this.transform.position.y), 1 << LayerMask.NameToLayer("Wall"));

        if (top || bot || mid)
        {
            PlayerControlsStart.againstWallRight = true;
            PlayerControls.againstWallRight = true;
            PlayerControlsDoubleJump.againstWallRight = true;
            PlayerControlsCling.againstWallRight = true;
            PlayerControlsBlink.againstWallRight = true;
            PlayerControlsDisoriented.againstWallRight = true;
            PlayerControlsTrue.againstWallRight = true;
        }
        else
        {
            PlayerControlsStart.againstWallRight = false;
            PlayerControls.againstWallRight = false;
            PlayerControlsDoubleJump.againstWallRight = false;
            PlayerControlsCling.againstWallRight = false;
            PlayerControlsBlink.againstWallRight = false;
            PlayerControlsDisoriented.againstWallRight = false;
            PlayerControlsTrue.againstWallRight = false;
        }
    }
}
