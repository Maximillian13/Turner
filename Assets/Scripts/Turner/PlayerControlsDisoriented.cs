// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControlsDisoriented : MonoBehaviour
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
    private const float MaxSpeed = 3;
    private const float SlowDownSpeed = 20;
    private const float Speed = 25;

    public static bool grounded;
    public static bool againstWallRight;
    public static bool againstWallLeft;
    public static bool killPlayer;

    private Animator anim;
    private Rigidbody2D ridg;
    //private GameObject camGO;
    //private Camera cam;

    void Start()
    {
        // Gets the animator on this object
        anim = GetComponent<Animator>();
        anim.speed = .5f;
        ridg = GetComponent<Rigidbody2D>();
        //camGO = GameObject.Find("Main Camera");
        //cam = camGO.GetComponent<Camera>();

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
                //if (ridg.velocity.x < MaxSpeed && ridg.velocity.x > -MaxSpeed)
                //{
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
                //}
                //
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
            #region GayMovment
            //// If not paused
            //if (Time.timeScale == 1)
            //{
            //    if (ridg.velocity.x <= 10 && ridg.velocity.x >= -10)
            //    {
            //        // Move the player
            //        if (Input.GetAxis("Horizontal") == -1 && againstWallLeft == false)
            //        {
            //            ridg.AddForce(new Vector2(-10, 0));
            //            if (flippedLeft == false)
            //            {
            //                FlipSpriteLeft();
            //            }
            //        }
            //        else if (Input.GetAxis("Horizontal") == 1 && againstWallRight == false)
            //        {
            //            ridg.AddForce(new Vector2(10, 0));
            //            if (flippedRight == false)
            //            {
            //                FlipSpriteRight();
            //            }
            //        }

            //        // To decelerate player
            //        if (Input.GetAxis("Horizontal") > .5f && ridg.velocity.x >= -10 && ridg.velocity.x < 0 && againstWallLeft == false && againstWallRight == false)
            //        {
            //            ridg.AddForce(new Vector2(10, 0));
            //            if (ridg.velocity.x >= -5 && ridg.velocity.x < 0 && grounded == true)
            //            {
            //                ridg.velocity = new Vector2(0, ridg.velocity.y);
            //            }
            //            Debug.Log("In");
            //        }

            //        if (Input.GetAxis("Horizontal") < .5f && ridg.velocity.x <= 10 && ridg.velocity.x > 0 && againstWallLeft == false && againstWallRight == false)
            //        {
            //            ridg.AddForce(new Vector2(-10, 0));
            //            if (ridg.velocity.x <= 5 && ridg.velocity.x > 0 && grounded == true)
            //            {
            //                ridg.velocity = new Vector2(0, ridg.velocity.y);
            //            }
            //            Debug.Log("In");
            //        }
            //    }
            //}
            #endregion

            if (killPlayer == true)
            {
                // Put in animation eventually
                this.ridg.isKinematic = true;
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

        // Brings the player back down faster
        if (grounded == false && Time.timeScale == 1)
        {
            ridg.AddForce(new Vector2(0, -10));
        }
    }

    void Update()
    {
        //if (Input.GetAxisRaw("ChangeCam") < 0 && Time.time > oldTime)
        //{
        //    if (camOnPlayer == false)
        //    {
        //        cam.gameObject.transform.parent = this.transform;
        //        cam.transform.localPosition = new Vector3(0, 0, -10);
        //        camOnPlayer = true;
        //    }
        //    else
        //    {
        //        cam.transform.parent = null;
        //        cam.transform.position = new Vector3(0, 0, -10);
        //        camOnPlayer = false;
        //    }
        //    oldTime = Time.time + .25f;
        //}
        // If not paused
        if (Time.timeScale == 1)
        {
            // Bumps up the time for the long idle to activate
            longIdleTimer += Time.deltaTime;

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

            // Check if Turner has been standing around for over 10 seconds
            anim.SetFloat("longIdle", longIdleTimer);

            // Check if Turner is up against a wall, in air
            if (grounded == false && againstWallLeft == true)
            {
                anim.SetBool("wallSlide", true);
                if (transform.localScale.x == 1)
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
            if (grounded == false && againstWallRight == true)
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
                if (timer2 >= .25f)
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