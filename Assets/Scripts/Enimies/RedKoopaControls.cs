// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class RedKoopaControls : MonoBehaviour 
{
    // Public
    public float speed;
    // Private
    private float slowSpeed;
    private float fastSpeed;
    private int direction;
    private float oldTime;
    private bool hitWall;
    private bool groundedLeft;
    private bool groundedRight;
    private bool spotted;
    private float spottedTimer;

    private GameObject laserSpriteRight;
    private GameObject laserSpriteLeft;

	void Start () 
    {
        // Set defaults
        slowSpeed = speed;
        fastSpeed = speed * 2;
        direction = 1;
        oldTime = 0;
        hitWall = false;
        groundedLeft = false;
        groundedRight = false;
        spotted = false;
        spottedTimer = 0;

        laserSpriteRight = this.transform.GetChild(0).gameObject;
        laserSpriteLeft = this.transform.GetChild(1).gameObject;
        laserSpriteLeft.gameObject.SetActive(false);

        
	}
	
	void FixedUpdate () 
    {
        // Check walls
        Debug.DrawLine(new Vector2(this.transform.position.x - .4f, this.transform.position.y - .3f), new Vector2(this.transform.position.x + .7f, this.transform.position.y - .3f), Color.blue);
        hitWall = Physics2D.Linecast(new Vector2(this.transform.position.x - .4f, this.transform.position.y - .3f), new Vector2(this.transform.position.x + .7f, this.transform.position.y - .3f), 1 << LayerMask.NameToLayer("Wall"));

        // Check edge (left)
        Debug.DrawLine(new Vector2(this.transform.position.x - .2f, this.transform.position.y), new Vector2(this.transform.position.x - .2f, this.transform.position.y - .75f), Color.red);
        groundedLeft = Physics2D.Linecast(new Vector2(this.transform.position.x - .75f, this.transform.position.y), new Vector2(this.transform.position.x - .75f, this.transform.position.y - .75f), 1 << LayerMask.NameToLayer("Wall"));

        // Check edge (Right)
        Debug.DrawLine(new Vector2(this.transform.position.x + .5f, this.transform.position.y), new Vector2(this.transform.position.x + .5f, this.transform.position.y - .75f), Color.red);
        groundedRight = Physics2D.Linecast(new Vector2(this.transform.position.x + .75f, this.transform.position.y), new Vector2(this.transform.position.x + .75f, this.transform.position.y - .75f), 1 << LayerMask.NameToLayer("Wall"));

        // Sight
        // If moving right
        if(direction > 0)
        {
            Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + 4, this.transform.position.y), Color.magenta);
            spotted = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + 4, this.transform.position.y), 1 << LayerMask.NameToLayer("Player"));
            laserSpriteRight.gameObject.SetActive(true);
            laserSpriteLeft.gameObject.SetActive(false);
        }
        // If moving left
        else
        {
            Debug.DrawLine(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x - 4, this.transform.position.y), Color.magenta);
            spotted = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x - 4, this.transform.position.y), 1 << LayerMask.NameToLayer("Player"));

            laserSpriteRight.gameObject.SetActive(false);
            laserSpriteLeft.gameObject.SetActive(true);
        }

        if(spotted == true)
        {
            //speed = .15f;
            spottedTimer = Time.time + 2;
        }

        // Changes the speed 
        if(spottedTimer >= Time.time)
        {
            speed = fastSpeed;
        }
        else
        {
            speed = slowSpeed;
        }

        // Check to see if on edge or hitting wall
        if (hitWall == true || groundedLeft == false || groundedRight == false)
        {
            // This is horse shit buttfuckit
            if (oldTime <= Time.time && spottedTimer <= Time.time)
            {
                if (groundedLeft != true && groundedRight != true)
                {
                    // I dont know why this makes it work but what ever.
                }
                else
                {
                    direction *= -1;
                    oldTime = Time.time + .25f;
                }
            }
        }

        this.transform.Translate(new Vector3(speed * direction, 0, 0));
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
