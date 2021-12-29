// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class GreenKoopaControls : MonoBehaviour
{
    // Public
    public int direction;

    // Private
    private float speed;
    private float xSpeed;
    private float ySpeed;
    private int gravScale;
    private bool onTop;
    private bool onBottom;
    private bool onLeft;
    private bool onRight;
    private bool spotted;
    private Rigidbody2D ridg;

    void Start()
    {
        // Set all defaults
        speed = .1f;
        onTop = false;
        onBottom = false;
        onRight = false;
        onLeft = false;
        spotted = false;
        ridg = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Check top
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y + .8f), Color.yellow);
        onTop = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y + .8f), 1 << LayerMask.NameToLayer("Wall"));

        // Check bottom
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y - .8f), Color.yellow);
        onBottom = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y - .8f), 1 << LayerMask.NameToLayer("Wall"));

        // Check right
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + .75f, this.transform.position.y), Color.yellow);
        onRight = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + .75f, this.transform.position.y), 1 << LayerMask.NameToLayer("Wall"));

        // Check left
        Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x - .8f, this.transform.position.y), Color.yellow);
        onLeft = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x - .8f, this.transform.position.y), 1 << LayerMask.NameToLayer("Wall"));

        // Sight
        if (spotted == true)
        {
            speed = .15f;
        }
        else
        {
            speed = .1f;
        }

        // Check to see if on edge or hitting wall
        if (onBottom == true)
        {
            //this.transform.Translate(new Vector3(speed * direction, 0, 0));
            //ridg.gravityScale = 1;
            xSpeed = speed * direction;
            ySpeed = 0;
            gravScale = 1;
        }
        if (onRight == true) 
        {
            //this.transform.Translate(new Vector3(0, speed * direction, 0));
            //ridg.gravityScale = 0;
            xSpeed = 0;
            ySpeed = speed * direction;
            gravScale = 0;
        }
        if (onTop == true)
        {
            //this.transform.Translate(new Vector3(speed * -direction, 0, 0));
            //ridg.gravityScale = 0;
            xSpeed = speed * -direction;
            ySpeed = 0;
            gravScale = 0;
        }
        if (onLeft == true)
        {
            //this.transform.Translate(new Vector3(0, speed * -direction, 0));
            //ridg.gravityScale = 0;
            xSpeed = 0;
            ySpeed = speed * -direction;
            gravScale = 0;
        }
        // Specail cases
        if(onLeft == true && onBottom == true && direction == 1)
        {
            xSpeed = speed * direction;
            ySpeed = 0;
            gravScale = 1;
        }
        if (onLeft == true && onTop == true && direction == -1)
        {
            xSpeed = speed * -direction;
            ySpeed = 0;
            gravScale = 0;
        }

        // This makes the koopa go forward
        this.transform.Translate(new Vector3(xSpeed, ySpeed, 0));
        ridg.gravityScale = gravScale;

        if (onBottom == false && onTop == false && onLeft == false && onRight == false)
        {
            ridg.gravityScale = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerControlsStart.killPlayer = true;
            PlayerControls.killPlayer = true;
            PlayerControlsDoubleJump.killPlayer = true;
            PlayerControlsCling.killPlayer = true;
            PlayerControlsBlink.killPlayer = true;
        }
    }
}
