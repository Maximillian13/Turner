// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneChoice : MonoBehaviour
{
    public Text pressWToWakeUp;
    public Text pressAToGoBack;
    public Text pressDToLeave;
    public Image firstRow;
    public Image secondRow;
    public Sprite spanishRow;
    public bool darkWorld;
    private float oldTime;
    private float oldTime2;
    private bool wakeUp;
    private bool goBack;
    private bool leave;
    private bool readyToClick;
    private bool fadeAway;

    //Translation
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
        
        if(lang.Equals("spanish"))
        {
            // Translated
            pressAToGoBack.text = "Camina a la izquierda para regresar.";
            pressDToLeave.text = "Salta para despertar.";
            pressWToWakeUp.text = "Camina a la derecha part irte.";
            firstRow.sprite = spanishRow;
        }

        // Set everything to be invisible 
        firstRow.CrossFadeAlpha(0, 0, true);
        secondRow.CrossFadeAlpha(0, 0, true);
        pressWToWakeUp.CrossFadeAlpha(0, 0, true);
        pressAToGoBack.CrossFadeAlpha(0, 0, true);
        pressDToLeave.CrossFadeAlpha(0, 0, true);

        // Set the timers
        oldTime = Time.time;
        oldTime2 = Mathf.Infinity;

        // To see if its ready to continue
        readyToClick = false;
        wakeUp = false;
        goBack = false;
        leave = false;
        fadeAway = false;
    }

    void FixedUpdate()
    {
        if (readyToClick == false)
        {
            if (Time.time > oldTime + 2)
            {
                firstRow.CrossFadeAlpha(1, .8f, true);
            }
            if (Time.time > oldTime + 6)
            {
                secondRow.CrossFadeAlpha(1, .8f, true);
            }
            if (Time.time > oldTime + 8)
            {
                // Show text "Click any button"
                pressWToWakeUp.CrossFadeAlpha(1, .8f, true);
                pressAToGoBack.CrossFadeAlpha(1, .8f, true);
                pressDToLeave.CrossFadeAlpha(1, .8f, true);
                // Gets ready to go to update
                if (Time.time > oldTime + 10)
                {
                    readyToClick = true;
                }
            }
        }

        // Loads next level after two seconds
        if (Time.time > oldTime2 + 2)
        {
            if (goBack == true)
            {
                SceneManager.LoadScene(433);
            }
            if (leave == true)
            {
                SceneManager.LoadScene(440);
            }
            if (wakeUp == true)
            {
                SceneManager.LoadScene(438);
            }
        }
    }

    private void Update()
    {
        // If ready
        if (readyToClick == true && fadeAway == false)
        {
            // Go back
            if(Input.GetAxis("Horizontal") == -1)
            {
                goBack = true;
                oldTime2 = Time.time;
                fadeAway = true;
            }

            // Leave
            if (Input.GetAxis("Horizontal") == 1)
            {
                leave = true;
                oldTime2 = Time.time;
                fadeAway = true;
            }

            // Wake up
            if (Input.GetButtonDown("Jump"))
            {
                wakeUp = true;
                oldTime2 = Time.time;
                fadeAway = true;
            }
        }
        if(fadeAway == true)
        {
            firstRow.CrossFadeAlpha(0, .5f, true);
            secondRow.CrossFadeAlpha(0, .5f, true);
            pressWToWakeUp.CrossFadeAlpha(0, .5f, true);
            pressAToGoBack.CrossFadeAlpha(0, .5f, true);
            pressDToLeave.CrossFadeAlpha(0, .5f, true);
        }
    }
}