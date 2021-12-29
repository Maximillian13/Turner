using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JailEndControl : MonoBehaviour 
{
    public Image fader;
    public bool mainMenu;
    public bool darkWorld;
    public bool secretEnding;
    public Transform blocker;
    private float alpha;
    private float timer;
    private bool done;
    private bool loadLevel;

	// Use this for initialization
	void Start () 
    {
        done = false;
        alpha = 0;
        timer = Mathf.Infinity;
        blocker.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (done == true)
        {
            fader.color = new Color(0, 0, 0, alpha);
            alpha += .005f;
            if (fader.color.a >= 1)
            {
                loadLevel = true;
                done = false;
                timer = Time.time;
            }
            blocker.gameObject.SetActive(true);
        }

        if (loadLevel == true && Time.time > timer + 2)
        {
            PlayerPrefs.SetInt("BEATGAMESPECAIL", 1);

            if (secretEnding == true)
            {
                SceneManager.LoadScene(555);
            }
            else
            {

                if (darkWorld == false)
                {
                    if (mainMenu == true)
                    {
                        SceneManager.LoadScene(546);
                    }
                    else
                    {
                        SceneManager.LoadScene(548);
                    }
                }
                else
                {
                    if (mainMenu == true)
                    {
                        SceneManager.LoadScene(549);
                    }
                    else
                    {
                        SceneManager.LoadScene(552);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            done = true;
        }
    }
}
