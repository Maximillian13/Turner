using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour 
{
    public bool trueWorld;
    private Image fade;
    private float oldTime;
    private float alpha;

    void Start()
    {
        fade = this.GetComponent<Image>();
        if(trueWorld == true)
        {
            fade.color = new Color(255, 255, 255, 1);
        }
        else
        {
            fade.color = new Color(0, 0, 0, 1);
        }
        oldTime = Time.time;
        alpha = 1;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (Time.time > oldTime + .25f)
        {
            if (alpha > 0)
            {
                if (trueWorld == true)
                {
                    fade.color = new Color(255, 255, 255, alpha);
                }
                else
                {
                    fade.color = new Color(0, 0, 0, alpha);
                }
                alpha -= .005f;
            }
        }
	}
}
