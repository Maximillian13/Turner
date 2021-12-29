using UnityEngine;
using System.Collections;

public class FlipButton : MonoBehaviour 
{
    private float oldTime;
    private float rot;
	// Use this for initialization
	void Start () 
    {
        oldTime = Time.time + Random.Range(5f, 8f);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	    if(Time.time > oldTime)
        {
            this.transform.eulerAngles = new Vector3(0, 0, rot);
            rot += Random.Range(1f, 3f);
        }
        if(Time.time > oldTime + .5f)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            oldTime = Time.time + Random.Range(5f, 8f);
        }
	}
}
