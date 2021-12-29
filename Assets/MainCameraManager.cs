using UnityEngine;
using System.Collections;

public class MainCameraManager : MonoBehaviour {

    GameObject otherCamera;

	// Use this for initialization
	void Awake () 
    {
        otherCamera = GameObject.Find("Main Camera");
	    if(otherCamera != this.gameObject)
        {
            Destroy(otherCamera);
        }
	}
}
