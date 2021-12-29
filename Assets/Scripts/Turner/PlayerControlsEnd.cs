// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControlsEnd : MonoBehaviour
{
    // Fields 
    private bool slowdown;
    private const float MaxSpeed = 2;
    private const float SlowDownSpeed = 20;
    private const float Speed = 25;

    public static bool grounded;
    public static bool againstWallRight;
    public static bool againstWallLeft;
    public static bool killPlayer;

    private Animator anim;
    private Rigidbody2D ridg;
    private Transform cam;

    void Start()
    {
        // Gets the animator on this object
        anim = GetComponent<Animator>();
        anim.speed = .4f;
        ridg = GetComponent<Rigidbody2D>();

        // Set other stuff up
        grounded = true;
        killPlayer = false;
        againstWallRight = false;
        againstWallLeft = false;
        slowdown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If not paused
        if (Time.timeScale == 1)
        {
        // If on ground
            #region MovesPlayer
            // If not going to fast
            //if (ridg.velocity.x < MaxSpeed && ridg.velocity.x > -MaxSpeed)
            //{
            // Left
            if (this.ridg.velocity.x > -MaxSpeed)
            {
                if (Input.GetAxis("Horizontal") == -1 && againstWallLeft == false)
                {
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
        if (this.transform.position.x >= -5 && this.transform.position.x <= 0)
        {
            cam = this.transform.GetChild(3);
        }
        if(this.transform.position.x >= 13 && cam != null)
        {
            cam.parent = null;
        }
    }

    void Update()
    {
            anim.SetFloat("speed", this.ridg.velocity.magnitude);
            anim.SetBool("inAir", !grounded);
    }

    // This method will make the sprite change the direction it is looking at
    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}