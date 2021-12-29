// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneSeven : MonoBehaviour
{
    public Text anyButton;
    public Image firstRow;
    public Image secondRow;
    public Sprite spanishRow;
    public int part;
    public bool darkWorld;
    //public Image thirdRow;
    //public Sprite[] firstRowSprites = new Sprite[4];
    //public Sprite[] secondRowSprites = new Sprite[4];
    //public Sprite[] thirdRowSprites = new Sprite[6];
    //public Sprite[] fourthRowSprites = new Sprite[6];
    private float oldTime;
    private float oldTime2;
    private float skipTime;
    private bool readyToClick;
    private string lang;
    //private int counterFirstAndSecond;
    //private int counterThird;

    void Start()
    {

        if (SteamManager.Initialized)
        {
            lang = Steamworks.SteamUtils.GetSteamUILanguage();
        }
        else
        {
            lang = "english";
        }

        if(lang == "spanish")
        {
            if (spanishRow != null)
            {
                firstRow.sprite = spanishRow;
            }
        }


        // Set everything to be invisible 
        firstRow.CrossFadeAlpha(0, 0, true);
        secondRow.CrossFadeAlpha(0, 0, true);
        //thirdRow.CrossFadeAlpha(0, 0, true);
        anyButton.CrossFadeAlpha(0, 0, true);

        // Set the timers
        oldTime = Time.time;
        oldTime2 = Mathf.Infinity;

        // To see if its ready to continue
        readyToClick = false;

        // To change the pictures
        //counterFirstAndSecond = 0;
        //counterThird = 0;
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
                if (Time.time > oldTime + 6)
                {
                    secondRow.CrossFadeAlpha(1, .8f, true);
                }
                if (Time.time > oldTime + 8)
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
                if (Time.time > oldTime + 8)
                {
                    secondRow.CrossFadeAlpha(1, .8f, true);
                }
                if (Time.time > oldTime + 10)
                {
                    // Show text "Click any button"
                    anyButton.CrossFadeAlpha(1, .8f, true);
                    // Gets ready to go to update
                    readyToClick = true;
                }
            }
            else
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
        // First and second row
        //FirstAndSecondCut();

        // Forth row
        //ThirdCut();


        // Loads next level after two seconds
        if (Time.time > oldTime2 + 2)
        {
            if (part == 0)
            {
                if (darkWorld == false)
                {
                    SceneManager.LoadScene(422);
                }
                else
                {
                    SceneManager.LoadScene(432);
                }
            }
            else if (part == 1)
            {
                if (darkWorld == false)
                {
                    SceneManager.LoadScene(423);
                }
                else
                {
                    SceneManager.LoadScene(433);
                }
            }
            else if (part == 2)
            {
                if (darkWorld == false)
                {
                    SceneManager.LoadScene(424);
                }
                else
                {
                    SceneManager.LoadScene(434);
                }
            }
            else if (part == 3)
            {
                if (darkWorld == false)
                {
                    SceneManager.LoadScene(435);
                }
                else
                {
                    SceneManager.LoadScene(436);
                }
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
                    if (darkWorld == false)
                    {
                        SceneManager.LoadScene(422);
                    }
                    else
                    {
                        SceneManager.LoadScene(432);
                    }
                }
                else if (part == 1)
                {
                    if (darkWorld == false)
                    {
                        SceneManager.LoadScene(423);
                    }
                    else
                    {
                        SceneManager.LoadScene(433);
                    }
                }
                else if (part == 2)
                {
                    if (darkWorld == false)
                    {
                        SceneManager.LoadScene(424);
                    }
                    else
                    {
                        SceneManager.LoadScene(434);
                    }
                }
                else if (part == 3)
                {
                    if (darkWorld == false)
                    {
                        SceneManager.LoadScene(435);
                    }
                    else
                    {
                        SceneManager.LoadScene(436);
                    }
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
                anyButton.CrossFadeAlpha(0, 1.5f, true);
            }
        }
    }

    //private void ThirdCut()
    //{
    //    if (counterThird == 10)
    //    {
    //        thirdRow.sprite = thirdRowSprites[0];
    //    }
    //    //if (counterThird == 20)
    //    //{
    //    //    thirdRow.sprite = thirdRowSprites[1];
    //    //}
    //    //if (counterThird == 30)
    //    //{
    //    //    thirdRow.sprite = thirdRowSprites[2];
    //    //}
    //    //if (counterThird == 40)
    //    //{
    //    //    thirdRow.sprite = thirdRowSprites[3];
    //    //}
    //    //if (counterThird == 50)
    //    //{
    //    //    thirdRow.sprite = thirdRowSprites[4];
    //    //}
    //    if (counterThird == 60)
    //    {
    //        thirdRow.sprite = thirdRowSprites[5];
    //        counterThird = 0;
    //    }
    //    counterThird++;
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