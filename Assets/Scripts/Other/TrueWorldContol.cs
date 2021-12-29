using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrueWorldContol : MonoBehaviour 
{
    public GameObject page;
    public Image fader;
    public Image bigPage;
    public Text pressEnter;
    private SpriteRenderer pageSprite;
    private bool goThrough;
    private float alpha;
    private float oldTime;
    private float oldTime2;
    private float oldTime3;
	
    // Hide everything
	void Start () 
    {
        oldTime = Time.time;
        oldTime2 = Mathf.Infinity;
        oldTime3 = Mathf.Infinity;
        alpha = 0;
        goThrough = true;
        bigPage.gameObject.SetActive(false);
        pressEnter.gameObject.SetActive(false);
        pageSprite = page.GetComponent<SpriteRenderer>();
        page.SetActive(false);
	}

    // If the page has been picked up end the game.
    void FixedUpdate()
    {
        // Get fade ready
        if(pageSprite.enabled == false && goThrough == true)
        {
            oldTime2 = Time.time;
            goThrough = false;
        }
        // fade
        if(Time.time >= oldTime2 + 3)
        {
            //fader.CrossFadeAlpha(1, 2, true);
            fader.color = new Color(0, 0, 0, alpha);
            alpha += .0025f;
            if (fader.color.a >= 1 && oldTime3 == Mathf.Infinity)
            {
                oldTime3 = Time.time;
            }
        } 
        // keep black for 3 seconds
        if(Time.time >= oldTime3 + 3)
        {
            PlayerPrefs.SetInt("PAGENUMBER20", 1);
            SceneManager.LoadScene(436);
        }
    }

    // Spawn the page if turner is on the other side of the room
    void OnTriggerEnter2D(Collider2D other)
    {
        if(Time.time >= oldTime + 4)
        {
            //bigPage.gameObject.SetActive(true);
            //pressEnter.gameObject.SetActive(true);
            page.SetActive(true);
        }
    }

}
