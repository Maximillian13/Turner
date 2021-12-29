// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneThree : MonoBehaviour
{
    public Text anyButton;
    public Image firstRow;
    public Image secondRow;
    public Image thirdRow;
    public Image fourthRow;
    public bool darkWorld;
    //public Sprite[] firstRowSprites = new Sprite[4];
    //public Sprite[] secondRowSprites = new Sprite[4];
    //public Sprite[] fourthRowSprites = new Sprite[6];
    private float oldTime;
    private float oldTime2;
    private float skipTime;
    private bool readyToClick;
    //private int counterFirstAndSecond;
    //private int counterFourth;

    // Translation
    public Sprite[] langSprite;
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
            fourthRow.sprite = langSprite[1];
        }
        else
        {
            fourthRow.sprite = langSprite[0];
        }

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

        // To change the pictures
        //counterFirstAndSecond = 0;
        //counterFourth = 0;
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
            if (Time.time > oldTime + 14)
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
        // First and second row
        //FirstAndSecondCut();

        // Forth row
        //FourthCut();


        // Loads next level after two seconds
        if (Time.time > oldTime2 + 2)
        {
            if (darkWorld == false)
            {
                SceneManager.LoadScene(105);
            }
            else
            {
                SceneManager.LoadScene(310);
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
                if (darkWorld == false)
                {
                    SceneManager.LoadScene(105);
                }
                else
                {
                    SceneManager.LoadScene(310);
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
                secondRow.CrossFadeAlpha(0, 1.5f, true);
                thirdRow.CrossFadeAlpha(0, 1.5f, true);
                fourthRow.CrossFadeAlpha(0, 1.5f, true);
                anyButton.CrossFadeAlpha(0, 1.5f, true);
            }
        }
    }

    //private void FourthCut()
    //{
    //    if (counterFirstAndSecond == 20)
    //    {
    //        fourthRow.sprite = fourthRowSprites[0];
    //    }
    //    if (counterFirstAndSecond == 40)
    //    {
    //        fourthRow.sprite = fourthRowSprites[1];
    //    }
    //    if (counterFirstAndSecond == 60)
    //    {
    //        fourthRow.sprite = fourthRowSprites[2];
    //    }
    //    counterFourth++;
    //}

    //private void FirstAndSecondCut()
    //{
    //    if (counterFirstAndSecond == 15)
    //    {
    //        firstRow.sprite = firstRowSprites[0];
    //        secondRow.sprite = secondRowSprites[0];
    //    }
    //    if (counterFirstAndSecond == 30)
    //    {
    //        firstRow.sprite = firstRowSprites[1];
    //        secondRow.sprite = secondRowSprites[1];
    //    }
    //    if (counterFirstAndSecond == 45)
    //    {
    //        firstRow.sprite = firstRowSprites[2];
    //        secondRow.sprite = secondRowSprites[2];
    //    }
    //    if (counterFirstAndSecond == 60)
    //    {
    //        firstRow.sprite = firstRowSprites[3];
    //        secondRow.sprite = secondRowSprites[3];
    //        counterFirstAndSecond = 0;
    //    }
    //    counterFirstAndSecond++;
    //}
}