using UnityEngine;
using System.Collections;

public class AttachCam : MonoBehaviour 
{
    private BoxCollider2D boxCol;
	// Use this for initialization
	void Start () 
    {
        boxCol = this.GetComponent<BoxCollider2D>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            this.transform.parent = other.transform;
            this.transform.localPosition = new Vector3(0, 0, -10);
            boxCol.enabled = false;
        }
    }
}
