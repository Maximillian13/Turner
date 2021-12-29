// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class BoxButton : MonoBehaviour 
{
    // Public 
    public GameObject[] blocker = new GameObject[0];
    public Sprite[] buttonUpDown = new Sprite[2];
    // Private 
    private SpriteRenderer buttonSprite;
    private string tagOfWhatsIn;
    private bool[] originalActiveState;
    private bool goThroughAgian;
    private bool activeButton;

	void Start()
    {
        // Set all the stuff correct
        goThroughAgian = false;
        activeButton = true;
        tagOfWhatsIn = null;
        buttonSprite = this.GetComponent<SpriteRenderer>();
        buttonSprite.sprite = buttonUpDown[0];
        MakeOriginalState();
    }

    private void MakeOriginalState()
    {
        originalActiveState = new bool[blocker.Length];
        for (int x = 0; x < originalActiveState.Length; x++)
        {
            originalActiveState[x] = blocker[x].activeSelf;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // So the button does not get turned off and on while standing on it and something else touches it
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
                buttonSprite.sprite = buttonUpDown[0];
                goThroughAgian = true;
                tagOfWhatsIn = null;
            }
        }
    }

    private void SetToOriginalState()
    {
        for (int x = 0; x < originalActiveState.Length; x++)
        {
            blocker[x].SetActive(originalActiveState[x]);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (goThroughAgian == true)
        {
            if (tagOfWhatsIn == null || tagOfWhatsIn == other.tag)
            {
                if ((other.tag == "Box" || other.tag == "Player") && activeButton == true)
                {
                    ChangeState();
                    buttonSprite.sprite = buttonUpDown[1];
                    tagOfWhatsIn = other.tag;
                }
                if (other.tag == "Circle")
                {
                    for (int x = 0; x < blocker.Length; x++)
                    {
                        blocker[x].SetActive(!blocker[x].activeSelf);
                    }
                    Destroy(other.gameObject, .5f);
                    activeButton = false;
                    buttonSprite.sprite = buttonUpDown[1];
                }
            }
            goThroughAgian = false;
        }
    }

    // Changes the block to turn off or on
    private void ChangeState()
    {
        for (int x = 0; x < blocker.Length; x++)
        {
            blocker[x].SetActive(!blocker[x].activeSelf);
        }
    }
}
