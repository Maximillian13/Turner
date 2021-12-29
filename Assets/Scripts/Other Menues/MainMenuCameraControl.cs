// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class MainMenuCameraControl : MonoBehaviour 
{
    private Camera cam;
    private bool goingDown;

	// Use this for initialization
	void Start () 
    {
        cam = this.GetComponent<Camera>();
        goingDown = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if(cam.orthographicSize <= 15)
        {
            goingDown = false;
        }
        if(cam.orthographicSize >= 20)
        {
            goingDown = true;
        }

        if (goingDown == true)
        {
            cam.orthographicSize -= .005f;
        }
        else
        {
            cam.orthographicSize += .005f;
        }
	}
}
