// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour 
{

    public Rigidbody2D backgroundMover;
    public Button keepWorking;
    public Button stopWorking;
    public Image blackOut;
    private Text keepWorkingText;
    private Text stopWorkingText;
    private bool allowDown;
    private bool moving;
    private float oldTime;
    private float speed = .6f;
    private float maxSpeed = .4f;
    private float time;
    private float escapeTimer;

    // Translation
    private string lang;

    void Start()
    {
        // Translation
        if (SteamManager.Initialized)
        {
            lang = Steamworks.SteamUtils.GetSteamUILanguage();
        }
        else
        {
            lang = "english";
        }

        keepWorkingText = keepWorking.GetComponentInChildren<Text>();
        stopWorkingText = stopWorking.GetComponentInChildren<Text>();
        
        if(lang.Equals("spanish"))
        {
            // Translated
            keepWorkingText.text = "Sigue Trabajando";
            stopWorkingText.text = "Detener Trabajando";
        }


        oldTime = Time.time;
        allowDown = true;
        moving = true;
        time = 0;
        blackOut.gameObject.SetActive(false);
        keepWorking.gameObject.SetActive(false);
        stopWorking.gameObject.SetActive(false);
    }

    void Update()
    {
        escapeTimer += Time.deltaTime;
        if(moving == false && time > 2)
        {
            blackOut.gameObject.SetActive(false);    
        }
        if (moving == false && time > 4)
        {
            moving = true;
        }
        time += Time.deltaTime;


        if (moving == true)
        {
            if (backgroundMover.transform.position.y > -22)
            {
                if (Time.time >= oldTime + 2)
                {
                    // Move the background
                    if (backgroundMover.velocity.magnitude <= maxSpeed)
                    {
                        backgroundMover.AddForce(transform.up * -speed);
                    }
                }
            }
            else
            {
                keepWorking.gameObject.SetActive(true);
                stopWorking.gameObject.SetActive(true);
            }
            // esc Key
            if (Input.GetButtonDown("Pause") && allowDown == true && escapeTimer > 2)
            {
                backgroundMover.position = new Vector2(0, -21);
                allowDown = false;
            }
        }
    }
}
