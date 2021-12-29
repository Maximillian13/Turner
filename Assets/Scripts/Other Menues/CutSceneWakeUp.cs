// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneWakeUp : MonoBehaviour
{
    public Text anyButton;
    public Image firstRow;
    public int part;
    private float oldTime;
    private float oldTime2;
    private float skipTime;
    private bool readyToClick;

    void Start()
    {
        // Set everything to be invisible 
        firstRow.CrossFadeAlpha(0, 0, true);
        anyButton.CrossFadeAlpha(0, 0, true);

        // Set the timers
        oldTime = Time.time;
        oldTime2 = Mathf.Infinity;

        // To see if its ready to continue
        readyToClick = false;
    }

    void FixedUpdate()
    {
        if (readyToClick == false)
        {
            if (part == 0)
            {
                if (Time.time > oldTime + 2)
                {
                    firstRow.CrossFadeAlpha(1, .8f, true);
                }
                if (Time.time > oldTime + 4)
                {
                    // Show text "Click any button"
                    anyButton.CrossFadeAlpha(1, .8f, true);
                    // Gets ready to go to update
                    readyToClick = true;
                }
            }
            else if (part == 1)
            {
                if (Time.time > oldTime + 1)
                {
                    firstRow.CrossFadeAlpha(1, .8f, true);
                }
                if (Time.time > oldTime + 4)
                {
                    // Show text "Click any button"
                    anyButton.CrossFadeAlpha(1, .8f, true);
                    // Gets ready to go to update
                    readyToClick = true;
                }
            }
        }

        // Loads next level after two seconds
        if (Time.time > oldTime2 + 2)
        {
            if (part == 0)
            {
                 SceneManager.LoadScene(439);
            }
            else
            {
                 SceneManager.LoadScene(437);
            }
        }
    }

    private void Update()
    {
        // For skipping
        if (Input.GetButton("Pause"))
        {
            skipTime += Time.deltaTime;
            if (skipTime >= 1f)
            {
                if (part == 0)
                {
                    SceneManager.LoadScene(439);
                }
                else
                {
                    SceneManager.LoadScene(437);
                }
            }
        }
        else
        {
            skipTime = 0;
        }

        // If ready
        if (readyToClick == true)
        {
            // Any button clicked
            if ((Input.anyKeyDown == true) || Input.GetButtonDown("Jump") || Input.GetButton("Interact") || Input.GetButtonDown("Pause"))
            {
                oldTime2 = Time.time;
                firstRow.CrossFadeAlpha(0, 1.5f, true);
                anyButton.CrossFadeAlpha(0, 1.5f, true);
            }
        }
    }
}