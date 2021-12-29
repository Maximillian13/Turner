// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour 
{
    // Fields 
    private bool jumpCoolDown;
    private bool wallJumpCoolDown;
    private bool dontJump;
    private bool slowdown;
    private bool wallSlideSpriteFlipedLeftWall;
    private bool wallSlideSpriteFlipedRightWall;
    //private bool camOnPlayer;
    //private float oldTime;
    private float timer;
    private float timer2;
    private float longIdleTimer;
    private const float MaxSpeed = 6;
    private const float SlowDownSpeed = 20;
    private const float Speed = 25;

    public static bool grounded;
    public static bool groundedLeft;
    public static bool groundedRight;
    public static bool slant;
    public static bool slantTwo;
    public static float direction;
    public static bool againstWallRight;
    public static bool againstWallLeft;
    public static bool killPlayer;

    private Animator anim;
    private Rigidbody2D ridg;
    //private GameObject camGO;
    //private Camera cam;
    private Image deathFade;
	

	void Start () 
    {
        direction = this.transform.localScale.x;
        // Gets the animator on this object
        anim = GetComponent<Animator>();
        ridg = GetComponent<Rigidbody2D>();
        //camGO = GameObject.Find("Main Camera");
        //cam = camGO.GetComponent<Camera>();

        // Sets the fade if fade out stuff
        GameObject deathFadeGO = GameObject.Find("DeathFade");
        deathFade = deathFadeGO.GetComponent<Image>();
        deathFade.color = new Color(0, 0, 0, 1);
        deathFade.CrossFadeAlpha(0, .5f, false);

        // Set other stuff up
        grounded = true;
        dontJump = false;
        killPlayer = false;
        againstWallRight = false;
        againstWallLeft = false;
        jumpCoolDown = false;
        wallJumpCoolDown = false;
        wallSlideSpriteFlipedLeftWall = false;
        wallSlideSpriteFlipedRightWall = false;
        slowdown = false;
        timer = 0;
        timer2 = 0;
        longIdleTimer = 0;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        // If not paused
        if (Time.timeScale == 1)
        {
            // If on ground
            if (grounded == true)
            {
                #region MovesPlayer
                // If not going to fast
                // Left
                if (this.ridg.velocity.x > -MaxSpeed)
                {
                    if (Input.GetAxis("Horizontal") == -1 && againstWallLeft == false)
                    {
                        longIdleTimer = 0;
                        slowdown = true;
                        ridg.AddForce(new Vector2(-Speed, 0));
                        // If facing the wrong direction
                        if (transform.localScale.x == 1)
                        {
                            FlipSprite();
                        }
                    }
                }

                // Right
                if (this.ridg.velocity.x < MaxSpeed)
                {
                    if (Input.GetAxis("Horizontal") == 1 && againstWallRight == false)
                    {
                        longIdleTimer = 0;
                        slowdown = true;
                        ridg.AddForce(new Vector2(Speed, 0));
                        // If facing the wrong direction
                        if (transform.localScale.x == -1)
                        {
                            FlipSprite();
                        }
                    }
                }
                #endregion

                #region SlowsPlayer
                if (Input.GetAxis("Horizontal") > -.9f && Input.GetAxis("Horizontal") < .9f && slowdown == true)
                {
                    if (ridg.velocity.x < 0)
                    {
                        ridg.AddForce(new Vector2(SlowDownSpeed, 0));
                    }
                    if (ridg.velocity.x > 0)
                    {
                        ridg.AddForce(new Vector2(-SlowDownSpeed, 0));
                    }
                    if (ridg.velocity.x > -1 && ridg.velocity.x < 1)
                    {
                        ridg.velocity = new Vector2(0, ridg.velocity.y);
                        slowdown = false;
                    }
                }
                #endregion
            }
            // In air
            else
            {
                
                #region MovesPlayerInAir
                // Left
                if (Input.GetAxis("Horizontal") == -1 && againstWallLeft == false && wallJumpCoolDown == false)
                {
                    longIdleTimer = 0;
                    ridg.AddForce(new Vector2(-Speed, 0));
                    if (transform.localScale.x == 1)
                    {
                        FlipSprite();
                    }
                }
                // Right
                else if (Input.GetAxis("Horizontal") == 1 && againstWallRight == false && wallJumpCoolDown == false) 
                {
                    longIdleTimer = 0;
                    ridg.AddForce(new Vector2(Speed, 0));
                    if (transform.localScale.x == -1)
                    {
                        FlipSprite();
                    }
                }
                #endregion

                #region SlowsPlayerInAir
                // Slow them down if they are too speedy
                if (ridg.velocity.x > MaxSpeed)
                {
                    //ridg.position = new Vector2(ridg.position.x, ridg.position.y);
                    ridg.AddForce(new Vector2(-SlowDownSpeed, 0));
                }
                if (ridg.velocity.x < -MaxSpeed)
                {
                    //ridg.position = new Vector2(ridg.position.x, ridg.position.y);
                    ridg.AddForce(new Vector2(SlowDownSpeed, 0));
                }
                #endregion
            }

            if(killPlayer == true)
            {
                // Put in animation eventually
                this.ridg.isKinematic = true;
                deathFade.CrossFadeAlpha(1, .3f, false);
                if (deathFade.color.a >= 1)
                {
                    killPlayer = false;
                    //Find & destroy all objects in scene
                    Transform[] allObjects;
                    allObjects = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];

                    foreach (Transform t in allObjects)
                    {
                        if (t.name != "Main Camera" && t.tag != "Music" && t.name != "SteamManager")
                        {
                            GameObject.Destroy(t.gameObject);
                        }
                    }
                    SceneManager.LoadSceneAsync(SceneManager.GetSceneAt(0).buildIndex, LoadSceneMode.Additive);
                }
            }
        }


        // Brings the player back down faster
        if (grounded == false && Time.timeScale == 1)
        {
            ridg.AddForce(new Vector2(0, -10));
        }
    }

    void Update()
    {
        // This is for slant
        direction = this.transform.localScale.x;
        //}
        // If not paused
        if (Time.timeScale == 1)
        {
            // Bumps up the time for the long idle to activate
            longIdleTimer += Time.deltaTime;

            #region WallSlide
            // Left
            if (againstWallLeft == true && groundedRight == false && wallJumpCoolDown == false)
            {
                longIdleTimer = 0;
                if (Input.GetButtonDown("Jump"))
                {
                    ridg.velocity = new Vector2(0, 0);
                    ridg.AddForce(new Vector2(300, 400));
                    if (transform.localScale.x == -1)
                    {
                        FlipSprite();
                    }
                    wallJumpCoolDown = true;
                    dontJump = true;
                }
            }

            // Right
            if (againstWallRight == true && groundedLeft == false && wallJumpCoolDown == false)
            {
                longIdleTimer = 0;
                if (Input.GetButtonDown("Jump"))
                {
                    ridg.velocity = new Vector2(0, 0);
                    ridg.AddForce(new Vector2(-300, 400));
                    if (transform.localScale.x == 1)
                    {
                        FlipSprite();
                    }
                    wallJumpCoolDown = true;
                    dontJump = true;
                }
            }
            #endregion

            #region Jumping
            // Keyboard and Controller  
            if (Input.GetButtonDown("Jump") && grounded == true && jumpCoolDown == false && dontJump == false)
            {
                longIdleTimer = 0;
                ridg.AddForce(new Vector2(0, 375));
                grounded = false;
                jumpCoolDown = true;
                //wallJumpCoolDown = true;
            }
            #endregion

            #region Animations
            // Find what animation should be running
            // See if the running animation should be played based of players magnitude
            anim.SetFloat("speed", this.ridg.velocity.magnitude);
             if (Input.GetAxis("Horizontal") > -.5f && Input.GetAxis("Horizontal") < .5f)
            {
                anim.SetFloat("speed", 0);
            }

            // Check if the Jump animation based on if the player is "not grounded"
            anim.SetBool("inAir", !grounded);

            // Check to see if it is slanted
            anim.SetBool("slant", slant);
            anim.SetBool("slantTwo", slantTwo);

            // Check if Turner has been standing around for over 10 seconds
            anim.SetFloat("longIdle", longIdleTimer);
            if (longIdleTimer >= 9)
            {
                longIdleTimer = 0;
            }

            // Check if Turner is up against a wall, in air
            if (grounded == false && againstWallLeft == true && wallJumpCoolDown == false)
            {
                anim.SetBool("wallSlide", true);
                if(transform.localScale.x == 1)
                {
                    FlipSprite();
                    wallSlideSpriteFlipedLeftWall = true;
                }
            }
            else
            {
                anim.SetBool("wallSlide", false);
                if (wallSlideSpriteFlipedLeftWall == true)
                {
                    wallSlideSpriteFlipedLeftWall = false;
                    //FlipSprite();
                }
            }
            if (grounded == false && againstWallRight == true && wallJumpCoolDown == false)
            {
                
                anim.SetBool("wallSlideRight", true);
                if (transform.localScale.x == -1)
                {
                    FlipSprite();
                    //Debug.Log("FLiped");
                    wallSlideSpriteFlipedRightWall = true;
                }
            }
            else
            {
                anim.SetBool("wallSlideRight", false);
                if (wallSlideSpriteFlipedRightWall == true)
                {
                    wallSlideSpriteFlipedRightWall = false;
                    //FlipSprite();
                }
            }
            
            #endregion

            #region JumpCoolDown
            // To limit how many jumps the play can do per second
            if (jumpCoolDown == true)
            {
                timer += Time.deltaTime;
                if (timer >= .5f)
                {
                    jumpCoolDown = false;
                    timer = 0;
                }
            }
            if (wallJumpCoolDown == true)
            {
                timer2 += Time.deltaTime;
                if(timer2 >= .25f)
                {
                    wallJumpCoolDown = false;
                    timer2 = 0;
                }
            }
            #endregion
            dontJump = false;
        }
        else
        {
            dontJump = true;
        }
    }

    // This method will make the sprite change the direction it is looking at
    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}