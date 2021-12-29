using UnityEngine;
using System.Collections;

public class FadeAway : MonoBehaviour 
{
    public float alpha;
    public float decayRate;
    public bool blinkAway;
    private SpriteRenderer sprite;
    private float oldTime;

	// Use this for initialization
	void Awake ()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, alpha);
        oldTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (blinkAway == false)
        {
            if (alpha >= 0)
            {
                sprite.color = new Color(1, 1, 1, alpha);
                alpha -= decayRate;
            }
        }
	    else
        {
            if(Time.time >= oldTime + decayRate)
            {
                sprite.color = new Color(1, 1, 1, 0);
            }
        }
	}
}