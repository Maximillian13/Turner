// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneFive : MonoBehaviour
{
    public Text anyButton;
    public Image firstRow;
    public Image secondRow;
    public Image thirdRow;
    public Image fourthRow;
    //public Sprite[] firstRowSprites = new Sprite[4];
    //public Sprite[] secondRowSprites = new Sprite[4];
    public Sprite[] thirdRowSprites = new Sprite[6];
    //public Sprite[] fourthRowSprites = new Sprite[6];
    public bool darkWorld;
    private float oldTime;
    private float oldTime2;
    private float skipTime;
    private bool readyToClick;
    //private int counterFirstAndSecond;
    private int counterThird;

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

        // To change the pictures
        //counterFirstAndSecond = 0;
        counterThird = 0;
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
        // First and second row
        //FirstAndSecondCut();

        // Forth row
        //ThirdCut();


        // Loads next level after two seconds
        if (Time.time > oldTime2 + 2)
        {
            if (darkWorld == false)
            {
                SceneManager.LoadScene(194);
            }
            else
            {
                SceneManager.LoadScene(399);
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
                    SceneManager.LoadScene(194);
                }
                else
                {
                    SceneManager.LoadScene(399);
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

    private void ThirdCut()
    {
        if (counterThird == 10)
        {
            thirdRow.sprite = thirdRowSprites[0];
        }
        //if (counterThird == 20)
        //{
        //    thirdRow.sprite = thirdRowSprites[1];
        //}
        //if (counterThird == 30)
        //{
        //    thirdRow.sprite = thirdRowSprites[2];
        //}
        //if (counterThird == 40)
        //{
        //    thirdRow.sprite = thirdRowSprites[3];
        //}
        //if (counterThird == 50)
        //{
        //    thirdRow.sprite = thirdRowSprites[4];
        //}
        if (counterThird == 60)
        {
            thirdRow.sprite = thirdRowSprites[5];
            counterThird = 0;
        }
        counterThird++;
    }

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