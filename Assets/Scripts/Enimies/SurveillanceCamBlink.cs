// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class SurveillanceCamBlink : MonoBehaviour
{
    // Public
    public int maxDegreeRange;
    public float blinkRate;
    public bool startOff;
    public Sprite[] warmUpSprite = new Sprite[3];
    // Private
    private SpriteRenderer camSprite;
    private float timer;
    private GameObject camEndPoint;
    private GameObject laserSprite;
    private bool otherWay;
    private bool hitPlayer;
    private float rayDistance;

    // Use this for initialization
    void Start()
    {
        // Set defaults
        timer = 0;
        otherWay = false;
        hitPlayer = false;
        camSprite = this.GetComponent<SpriteRenderer>();
        camEndPoint = this.transform.GetChild(0).gameObject;
        laserSprite = this.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (maxDegreeRange != 0)
        {
            if (otherWay == false) 
            {
                this.transform.Rotate(0, 0, 1);
                if (this.transform.localRotation.eulerAngles.z >= maxDegreeRange/*60*/ && this.transform.localRotation.eulerAngles.z < 360 - maxDegreeRange)
                {
                    otherWay = true;
                }
            }

            if (otherWay == true)
            {
                this.transform.Rotate(0, 0, -1);
                if (this.transform.localRotation.eulerAngles.z < 360 - maxDegreeRange && this.transform.localRotation.eulerAngles.z > maxDegreeRange + 1) //300
                {
                    otherWay = false;
                }
            }
        }
    }

    void Update()
    {
        if (startOff == false)
        {
            if (timer < blinkRate)
            {
                CameraDetection();
                laserSprite.SetActive(true);
                // Change sprite to dark red
                camSprite.sprite = warmUpSprite[2];
            }
            else
            {
                // Turn off sprite
                laserSprite.SetActive(false);
                // Change the sprite to yellow
                camSprite.sprite = warmUpSprite[0];
                // Change to light red
                if (timer > blinkRate * 1.6f)
                {
                    camSprite.sprite = warmUpSprite[1];
                }
            }
            // Increment timer
            timer += Time.deltaTime;
            if (timer > blinkRate * 2)
            {
                timer = 0;
            }
        }
        else
        {
            if (timer > blinkRate)
            {
                CameraDetection();
                laserSprite.SetActive(true);
                camSprite.sprite = warmUpSprite[2];
            }
            else
            {
                laserSprite.SetActive(false);
                camSprite.sprite = warmUpSprite[0];
                // Change to light red
                if (timer > blinkRate * .6f)
                {
                    camSprite.sprite = warmUpSprite[1];
                }
            }
            // Increment timer
            timer += Time.deltaTime;
            if (timer > blinkRate * 2)
            {
                timer = 0;
            }
        }
        // If the player is seen then dont test if there is a wall?
        if (hitPlayer == true)
        {
            PlayerControlsStart.killPlayer = true;
            PlayerControls.killPlayer = true;
            PlayerControlsDoubleJump.killPlayer = true;
            PlayerControlsCling.killPlayer = true;
        }
    }

    void CameraDetection()
    {
        // Get direction of other point
        Vector3 fromPosition = this.transform.position;
        Vector3 toPosition = camEndPoint.transform.position;
        Vector3 direction = toPosition - fromPosition;

        Debug.DrawRay(this.transform.position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction);
        // Makes it so it does not change if it hits the player
        rayDistance = hit.distance + .05f;

        hitPlayer = Physics2D.Raycast(this.transform.position, direction, rayDistance, 1 << LayerMask.NameToLayer("Player"));
        laserSprite.transform.localScale = new Vector3(laserSprite.transform.localScale.x, rayDistance * 2.5f, this.laserSprite.transform.position.z);

        if (hitPlayer == true)
        {
            PlayerControlsStart.killPlayer = true;
            PlayerControls.killPlayer = true;
            PlayerControlsDoubleJump.killPlayer = true;
            PlayerControlsCling.killPlayer = true;
        }
    }
}
