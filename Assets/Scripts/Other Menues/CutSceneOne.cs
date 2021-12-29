// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneOne : MonoBehaviour 
{
    public Text firstQuote;
    public Text secondQuote;
    public Text thirdQuote;
    public Text anyButton;
    public Image firstRow;
    public Image secondRow;
    public Image thirdRow;
    public Image fourthRow;
    public Sprite[] firstRowSprites = new Sprite[4];
    public Sprite[] secondRowSprites = new Sprite[4];
    public Sprite[] thirdRowSprites = new Sprite[4];
    public Sprite[] fourthRowSprites = new Sprite[6];
    public bool darkWorld;
    private float oldTime;
    private float oldTime2;
    private float skipTime;
    private bool partTwo;
    private bool readyToClick;
    private int counterFirstAndSecond;
    private int counterFourth;

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

        // Translated
        if (lang.Equals("spanish"))
        {
            firstQuote.text = "Necesitas regresar.";
            secondQuote.text = "El mundo no esta listo para ti.";
            thirdQuote.text = "Yo no estaba listo para ti.";
        }


        // Set everything to be invisible 
        firstQuote.CrossFadeAlpha(0, 0, true);
        secondQuote.CrossFadeAlpha(0, 0, true);
        thirdQuote.CrossFadeAlpha(0, 0, true);
        firstRow.CrossFadeAlpha(0, 0, true);
        secondRow.CrossFadeAlpha(0, 0, true);
        thirdRow.CrossFadeAlpha(0, 0, true);
        fourthRow.CrossFadeAlpha(0, 0, true);
        anyButton.CrossFadeAlpha(0, 0, true);

        // Set the timers
        oldTime = Time.time;
        oldTime2 = Mathf.Infinity;

        // To determine what part
        partTwo = false;
        // To see if its ready to continue
        readyToClick = false;

        // To change the pictures
        counterFirstAndSecond = 0;
        counterFourth = 0;
    }

    void FixedUpdate()
    {
        // Part one
        if (partTwo == false)
        {
            // Pop up the quotes
            if (Time.time > oldTime + 1)
            {
                firstQuote.CrossFadeAlpha(1, .8f, true);
            }
            if (Time.time > oldTime + 3)
            {
                secondQuote.CrossFadeAlpha(1, .8f, true);
            }
            if (Time.time > oldTime + 6)
            {
                thirdQuote.CrossFadeAlpha(1, .8f, true);
            }
            if (Time.time > oldTime + 9)
            {
                // Go into part two (Comic book)
                partTwo = true;
                // Reset time so we adding the numbers will be more clear
                oldTime = Time.time;
                // Hide all quotes
                firstQuote.CrossFadeAlpha(0, .8f, true);
                secondQuote.CrossFadeAlpha(0, .8f, true);
                thirdQuote.CrossFadeAlpha(0, .8f, true);
            }
        }
        else // Part two
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
            FirstAndSecondCut();
            // Third row
            //ThirdCut();
            // Forth row
            FourthCut();

        }
        // Loads next level after two seconds
        if(Time.time > oldTime2 + 2)
        {
            if (darkWorld == false)
            {
                SceneManager.LoadScene(5);
            }
            else
            {
                SceneManager.LoadScene(210);
            }
        }
    }

    private void Update()
    {
        // For skipping
        if(Input.GetButton("Pause"))
        {
            skipTime += Time.deltaTime;
            if (skipTime >= 1f)
            {
                if (darkWorld == false)
                {
                    SceneManager.LoadScene(5);
                }
                else
                {
                    SceneManager.LoadScene(210);
                }
            }
        }
        else
        {
            skipTime = 0;
        }
        // If ready
        if(readyToClick == true)
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

    private void FourthCut()
    {
        if (counterFirstAndSecond == 10)
        {
            fourthRow.sprite = fourthRowSprites[0];
        }
        if (counterFirstAndSecond == 20)
        {
            fourthRow.sprite = fourthRowSprites[1];
        }
        if (counterFirstAndSecond == 30)
        {
            fourthRow.sprite = fourthRowSprites[2];
        }
        if (counterFirstAndSecond == 40)
        {
            fourthRow.sprite = fourthRowSprites[3];
        }
        if (counterFirstAndSecond == 50)
        {
            fourthRow.sprite = fourthRowSprites[4];
        }
        if (counterFirstAndSecond == 60)
        {
            fourthRow.sprite = fourthRowSprites[5];
        }
        if (counterFirstAndSecond == 70)
        {
            fourthRow.sprite = fourthRowSprites[6];
        }
        if (counterFirstAndSecond == 80)
        {
            fourthRow.sprite = fourthRowSprites[7];
            counterFourth = 0;
        }
        counterFourth++;
    }

    private void FirstAndSecondCut()
    {
        if (counterFirstAndSecond == 15)
        {
            //firstRow.sprite = firstRowSprites[0];
            secondRow.sprite = secondRowSprites[0];
            thirdRow.sprite = thirdRowSprites[0];
        }
        if (counterFirstAndSecond == 30)
        {
            //firstRow.sprite = firstRowSprites[1];
            secondRow.sprite = secondRowSprites[1];
            thirdRow.sprite = thirdRowSprites[1];
        }
        if (counterFirstAndSecond == 45)
        {
            firstRow.sprite = firstRowSprites[2];
            secondRow.sprite = secondRowSprites[2];
            thirdRow.sprite = thirdRowSprites[2];
        }
        if (counterFirstAndSecond == 60)
        {
            //firstRow.sprite = firstRowSprites[3];
            secondRow.sprite = secondRowSprites[3];
            thirdRow.sprite = thirdRowSprites[3];
            counterFirstAndSecond = 0;
        }
        counterFirstAndSecond++;
    }
}