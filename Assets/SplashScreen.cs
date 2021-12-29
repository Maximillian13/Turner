using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    private Image splash;
    private float timer;
    private float timer2;
	// Use this for initialization
	void Start () 
    {
        splash = this.gameObject.GetComponent<Image>();
        splash.gameObject.SetActive(true);

        if(PlayerPrefs.GetInt("SPLASHSCREEN") == 0)
        {
            splash.gameObject.SetActive(true);
        }
        else
        {
            splash.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(PlayerPrefs.GetInt("SPLASHSCREEN") == 0)
        {
            if(timer < 4 && Input.anyKeyDown == false)
            {
                timer += Time.deltaTime;
            }
            else
            {
                PlayerPrefs.SetInt("SPLASHSCREEN", 1);
            }
        }
        else
        {
            splash.CrossFadeAlpha(0, .25f, true);
            timer2 += Time.deltaTime;
        }

        if(timer2 >= .5f)
        {
            splash.gameObject.SetActive(false);
        }
	}
}
