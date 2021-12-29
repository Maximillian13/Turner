// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PagePickUp : MonoBehaviour 
{
    // Fields 
    private GameObject pagePopUpGO;
    private GameObject pressEnter;
    private Image pagePopUp;
    private bool popUp;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCol;
    public int pageNumber;
    public static int pageNumberForAnyone;
    public static bool pickedUp;

	// Use this for initialization
	void Start () 
    {
        // Pop up
        pagePopUpGO = GameObject.Find("PagePopUp");
        pagePopUp = pagePopUpGO.GetComponent<Image>();
        pagePopUpGO.SetActive(false);
        pressEnter = GameObject.Find("PressEnter");
        pressEnter.SetActive(false);

        // For the page itself
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        boxCol = this.gameObject.GetComponent<BoxCollider2D>();

        popUp = false;

        pickedUp = false;
        pageNumberForAnyone = pageNumber;
        if (PlayerPrefs.GetInt("PAGENUMBER" + pageNumber) == 1)
        {
            if (pageNumber != 20)
            {
                Destroy(this.gameObject);
            }
        }
	}

    void Update()
    {
        if (popUp == true)
        {
            if (Input.GetButtonDown("Pause"))
            {
                pagePopUp.CrossFadeAlpha(0, .4f, true);
                pressEnter.SetActive(false);
                //Time.timeScale = 1;
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButton("Interact"))
            {
                pagePopUp.CrossFadeAlpha(0, .4f, true);
                pressEnter.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    // Sets the pickedUp value to true and that will make it so when the player leaves the level the PlayerPref is set
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Send to class "DoorToNextLevel" 
            pickedUp = true;

            // Hide the page
            sprite.enabled = false;
            boxCol.enabled = false;
            
            pagePopUpGO.SetActive(true);
            pressEnter.SetActive(true);
            popUp = true;
            Time.timeScale = 0;
        }
    }
}
