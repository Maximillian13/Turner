// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class BoxButtonTime : MonoBehaviour
{
    // Public
    public GameObject[] blocker = new GameObject[0];
    public Sprite[] buttonUpDown = new Sprite[2];
    // Public
    private SpriteRenderer[] blockerSpriteRenderer;
    private BoxCollider2D[] blockerBoxCol;
    public float timeButtonActive;
    private float oldTime;
    private float counter;
    private bool goThroughUpdate;
    private bool[] originalActiveState;
    private SpriteRenderer buttonSprite;
    private bool activeButton;

    void Start()
    {
        // Set all the stuff to what it should be
        oldTime = 0;
        counter = 0;
        goThroughUpdate = false;
        activeButton = true;
        // Set up the sprite renderer and collision of blockers
        blockerSpriteRenderer = new SpriteRenderer[blocker.Length];
        blockerBoxCol = new BoxCollider2D[blocker.Length];
        for (int x = 0; x < blocker.Length; x++)
        {
            blockerSpriteRenderer[x] = blocker[x].GetComponent<SpriteRenderer>();
            blockerBoxCol[x] = blocker[x].GetComponent<BoxCollider2D>();
        }
        // Button up = 0, Button down = 1
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

    private void SetToOriginalState()
    {
        for (int x = 0; x < originalActiveState.Length; x++)
        {
            blocker[x].SetActive(originalActiveState[x]);
        }
    }
    void FixedUpdate()
    {
        // Show the button change when time is getting close
        if (Time.time > oldTime - (timeButtonActive / 2) && goThroughUpdate == true)
        {
            if (counter % 8 == 0)
            {
                buttonSprite.sprite = buttonUpDown[0];
            }
            if (counter % 16 == 0)
            {
                buttonSprite.sprite = buttonUpDown[1];
            }
            counter++;
        }

        // Go in if time is up
        if (Time.time > oldTime && goThroughUpdate == true)
        {
            // Hide or show blockers
            SetBlockers();
            //SetToOriginalState();
            buttonSprite.sprite = buttonUpDown[0];
            activeButton = true;
            goThroughUpdate = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If it is good go in
        if ((other.tag == "Box" || other.tag == "Player" || other.tag == "Circle") && activeButton == true)
        {
            // Hide or show blockers
            SetBlockers();
            buttonSprite.sprite = buttonUpDown[1];
            activeButton = false;
        }
    }

    // Sets the time to work right
    void OnTriggerExit2D(Collider2D other)
    {
        if (activeButton == false)
        {
            oldTime = Time.time + timeButtonActive;
            goThroughUpdate = true;
        }
    }

    // Hide or show blockers
    void SetBlockers()
    {
        for (int x = 0; x < blocker.Length; x++)
        {
            //blocker[x].SetActive(!blocker[x].activeSelf);
            blockerSpriteRenderer[x].enabled = !blockerSpriteRenderer[x].enabled;
            blockerBoxCol[x].enabled = !blockerBoxCol[x].enabled;
        }
    }

}
