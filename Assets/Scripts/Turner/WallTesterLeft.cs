// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class WallTesterLeft : MonoBehaviour 
{
    private bool top;
    private bool bot;
    private bool mid;

    // Checks to see if Turner is up against the right wall
    void Update()
    {
        // Top Left side
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y + .35f), new Vector2(this.transform.position.x - .3f, this.transform.position.y + .35f), Color.red);
        // Bot Left side
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y - .35f), new Vector2(this.transform.position.x - .3f, this.transform.position.y - .35f), Color.red);
        // Mid Left Side
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x -.3f, this.transform.position.y), Color.red);

        // for all of them
        top = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y + .35f), new Vector2(this.transform.position.x - .3f, this.transform.position.y + .35f), 1 << LayerMask.NameToLayer("Wall"));
        bot = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y - .35f), new Vector2(this.transform.position.x - .3f, this.transform.position.y - .35f), 1 << LayerMask.NameToLayer("Wall"));
        mid = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x - .3f, this.transform.position.y), 1 << LayerMask.NameToLayer("Wall"));

        if(top || bot || mid)
        {
            PlayerControlsStart.againstWallLeft = true;
            PlayerControls.againstWallLeft = true;
            PlayerControlsDoubleJump.againstWallLeft = true;
            PlayerControlsCling.againstWallLeft = true;
            PlayerControlsBlink.againstWallLeft = true;
            PlayerControlsDisoriented.againstWallLeft = true;
            PlayerControlsTrue.againstWallLeft = true;
        }
        else
        {
            PlayerControlsStart.againstWallLeft = false;
            PlayerControls.againstWallLeft = false;
            PlayerControlsDoubleJump.againstWallLeft = false;
            PlayerControlsCling.againstWallLeft = false;
            PlayerControlsBlink.againstWallLeft = false;
            PlayerControlsDisoriented.againstWallLeft = false;
            PlayerControlsTrue.againstWallLeft = false;
        }

        // Low Left side
        //Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y - .35f), new Vector2(this.transform.position.x - .3f, this.transform.position.y - .35f), Color.red);
        //PlayerControls.againstWallLeft = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y - .35f), new Vector2(this.transform.position.x - .3f, this.transform.position.y - .35f), 1 << LayerMask.NameToLayer("Wall"));
    }
}
