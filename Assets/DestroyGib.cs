using UnityEngine;
using System.Collections;

public class DestroyGib : MonoBehaviour 
{
    float timer;

	void Start () 
    {
        timer = 0;
	}

	void Update () 
    {
        // If active
	    if(this.gameObject.activeInHierarchy == true)
        {
            timer += Time.deltaTime;
        }
        // after 5 seconds, destroy self
        if(timer > 5)
        {
            Destroy(this.gameObject);
        }
	}
}
