// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class SurveillanceCam : MonoBehaviour 
{
    // Public
    public int maxDegreeRange;
    // Private
    private GameObject camEndPoint;
    private GameObject laserSprite;
    private bool otherWay;
    private bool hitPlayer;
    private float rayDistance;

	// Use this for initialization
	void Start () 
    {
        // Set defaults
        otherWay = false;
        hitPlayer = false;
        camEndPoint = this.transform.GetChild(0).gameObject;
        laserSprite = this.transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (maxDegreeRange != 0)
        {
            if (otherWay == false)
            {

                this.transform.Rotate(0, 0, 1);
                if (this.transform.localRotation.eulerAngles.z >= maxDegreeRange && this.transform.localRotation.eulerAngles.z < 360 - maxDegreeRange)
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
        CameraDetection();
    }

    void CameraDetection()
    {
        // Get the direction of the end point
        Vector3 fromPosition = this.transform.position;
        Vector3 toPosition = camEndPoint.transform.position;
        Vector3 direction = toPosition - fromPosition;

        Debug.DrawRay(this.transform.position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction);
        // Makes it so it does not change if it hits the player
        rayDistance = hit.distance + .05f;

        hitPlayer = Physics2D.Raycast(this.transform.position, direction, rayDistance, 1 << LayerMask.NameToLayer("Player"));
        laserSprite.transform.localScale = new Vector3(laserSprite.transform.localScale.x, rayDistance * 2.6f, this.laserSprite.transform.position.z);

        if (hitPlayer == true)
        {
            PlayerControlsStart.killPlayer = true;
            PlayerControls.killPlayer = true;
            PlayerControlsDoubleJump.killPlayer = true;
            PlayerControlsCling.killPlayer = true;
        }
    }
}
