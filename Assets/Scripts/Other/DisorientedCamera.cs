using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisorientedCamera : MonoBehaviour 
{
    public Image fader;
    private float alpha;
	// Use this for initialization
	void Start () 
    {
        this.transform.Rotate(new Vector3(0, 0, Random.Range(-3f, 3f)));
        alpha = .5f;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        //this.transform.Rotate(new Vector3(0, 0, Random.Range(-1f, 1f)));
        if (fader != null)
        {
            fader.color = new Color(0, 0, 0, alpha);
        }
        if(alpha > .5)
        {
            alpha += Random.Range(-.2f, .1f);
        }
        else
        {
            alpha += Random.Range(-.1f, .2f);
        }
        //alpha += Random.Range(-.1f, .1f);
        // One choice
        if (this.transform.eulerAngles.z > 5)
        {
            this.transform.Rotate(new Vector3(0, 0, Random.Range(-3f, -1f)));
        }
        if (this.transform.eulerAngles.z > 350 && this.transform.eulerAngles.z > 180)
        {
            this.transform.Rotate(new Vector3(0, 0, Random.Range(1f, 3f)));
        }
        this.transform.Rotate(new Vector3(0, 0, Random.Range(-1f, 1f)));
	}
}
