// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class BoxButtonHideShow : MonoBehaviour
{
    // Public
    public GameObject[] blocker = new GameObject[0];
    // Private
    private SpriteRenderer[] hiders;
    public Sprite[] buttonUpDown = new Sprite[2];
    private SpriteRenderer buttonSprite;
    private string tagOfWhatsIn;
    private bool[] originalActiveState;
    private bool goThroughAgian;
    private bool activeButton;

    void Start()
    {
        // Turn all things to what they should be
        goThroughAgian = false;
        activeButton = true;
        buttonSprite = this.GetComponent<SpriteRenderer>();
        buttonSprite.sprite = buttonUpDown[0];
        tagOfWhatsIn = null;

        // Sites up the sprites to be hidden
        hiders = new SpriteRenderer[blocker.Length];
        for (int x = 0; x < hiders.Length; x++)
        {
            hiders[x] = blocker[x].GetComponent<SpriteRenderer>();
        }

        MakeOriginalState();
    }

    private void MakeOriginalState()
    {
        originalActiveState = new bool[blocker.Length];
        for (int x = 0; x < originalActiveState.Length; x++)
        {
            originalActiveState[x] = hiders[x].enabled;
        }
    }

    private void SetToOriginalState()
    {
        for (int x = 0; x < originalActiveState.Length; x++)
        {
            //blocker[x].SetActive(originalActiveState[x]);
            hiders[x].enabled = originalActiveState[x];
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (tagOfWhatsIn == null || tagOfWhatsIn == other.tag)
        {
            if ((other.tag == "Box" || other.tag == "Player" || other.tag == "Circle") && activeButton == true)
            {
                ChangeState();
                buttonSprite.sprite = buttonUpDown[1];
                tagOfWhatsIn = other.tag;
                goThroughAgian = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (tagOfWhatsIn == other.tag)
        {
            if ((other.tag == "Box" || other.tag == "Player") && activeButton == true)
            {
                SetToOriginalState();
                //ChangeState();
                buttonSprite.sprite = buttonUpDown[0];
                tagOfWhatsIn = null;
                goThroughAgian = true;
            }
        }
    }

    private void ChangeState()
    {
        for (int x = 0; x < hiders.Length; x++)
        {
            hiders[x].enabled = !hiders[x].enabled;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (goThroughAgian == true)
        {
            if (tagOfWhatsIn == null || tagOfWhatsIn == other.tag)
            {
                if ((other.tag == "Box" || other.tag == "Player" || other.tag == "Circle") && activeButton == true)
                {
                    ChangeState();
                    buttonSprite.sprite = buttonUpDown[1];
                    tagOfWhatsIn = other.tag;

                }
            }
            goThroughAgian = false;
        }
    }
}
