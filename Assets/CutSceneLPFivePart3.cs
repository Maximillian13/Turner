using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneLPFivePart3 : MonoBehaviour
{

    public Text anyButton;
    public Image firstRow;
    public Image secondRow;
    public Image thirdRow;
    public Image fourthRow;
    private float oldTime;
    private float oldTime2;
    private float skipTime;
    private bool readyToClick;

    void Start()
    {
        // Set everything to be invisible 
        firstRow.CrossFadeAlpha(0, 0, true);
        secondRow.CrossFadeAlpha(0, 0, true);
        thirdRow.CrossFadeAlpha(0, 0, true);
        fourthRow.CrossFadeAlpha(0, 0, true);
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
            if (Time.time > oldTime + 2)
            {
                firstRow.CrossFadeAlpha(1, .8f, true);
            }
            if (Time.time > oldTime + 6)
            {
                secondRow.CrossFadeAlpha(1, .8f, true);
            }
            if (Time.time > oldTime + 10)
            {
                thirdRow.CrossFadeAlpha(1, .8f, true);
            }
            if (Time.time > oldTime + 16)
            {
                fourthRow.CrossFadeAlpha(1, .8f, true);
            }
            if (Time.time > oldTime + 18)
            {
                // Show text "Click any button"
                anyButton.CrossFadeAlpha(1, .8f, true);
                // Gets ready to go to update
                readyToClick = true;
            }
        }


        // Loads next level after two seconds
        if (Time.time > oldTime2 + 2)
        {
            GameObject music = GameObject.FindWithTag("Music");
            if (music != null)
            {
                Destroy(music);
            }
            SceneManager.LoadScene(0);
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
                GameObject music = GameObject.FindWithTag("Music");
                if (music != null)
                {
                    Destroy(music);
                }
                SceneManager.LoadScene(0);
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
                secondRow.CrossFadeAlpha(0, 1.5f, true);
                thirdRow.CrossFadeAlpha(0, 1.5f, true);
                fourthRow.CrossFadeAlpha(0, 1.5f, true);
                anyButton.CrossFadeAlpha(0, 1.5f, true);
            }
        }
    }
}
