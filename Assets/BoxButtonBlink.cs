using UnityEngine;
using System.Collections;

public class BoxButtonBlink : MonoBehaviour 
{
    // Public 
    public GameObject[] blocker = new GameObject[0];
    public Sprite[] buttonUpDown = new Sprite[2];
    public int counter;
    // Private 
    private SpriteRenderer buttonSprite;
    private string tagOfWhatsIn;
    private bool activeButton;
    private bool goThroughAgian;
    private string[] originalName;
    private Color[] originalColor;
    private SpriteRenderer[] blockerSprite;

    void Start()
    {
        // Set all the stuff correct
        goThroughAgian = false;
        activeButton = true;
        tagOfWhatsIn = null;
        buttonSprite = this.GetComponent<SpriteRenderer>();
        buttonSprite.sprite = buttonUpDown[0];

        blockerSprite = new SpriteRenderer[blocker.Length];
        originalColor = new Color[blockerSprite.Length];
        originalName = new string[blockerSprite.Length];

        for (int x = 0; x < blockerSprite.Length; x++)
        {
            blockerSprite[x] = blocker[x].GetComponent<SpriteRenderer>();
        }

        MakeOriginalState();
    }

    private void MakeOriginalState()
    {
        for (int x = 0; x < blocker.Length; x++)
        {
            originalColor[x] = blockerSprite[x].color;
            originalName[x] = blocker[x].name;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // So the button does not get turned off and on while standing on it and something else touches it
        if (tagOfWhatsIn == null || tagOfWhatsIn == other.tag)
        {
            if ((other.tag == "Box" || other.tag == "Player") && activeButton == true)
            {
                ChangeState();
                buttonSprite.sprite = buttonUpDown[1];
                tagOfWhatsIn = other.tag;
                //activeButton = false;
                goThroughAgian = false;
            }
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

    void OnTriggerExit2D(Collider2D other)
    {
        if (tagOfWhatsIn == other.tag)
        {
            if ((other.tag == "Box" || other.tag == "Player") && activeButton == true)
            {
                SetToOriginalState();
                buttonSprite.sprite = buttonUpDown[0];
                tagOfWhatsIn = null;
                goThroughAgian = true;
                //activeButton = true;
            }
        }
    }

    private void SetToOriginalState()
    {
        for (int x = 0; x < blocker.Length; x++)
        {
            blockerSprite[x].color = originalColor[x];
            blocker[x].name = originalName[x];
        }
        counter++;
    }


    // Changes the block to turn off or on
    private void ChangeState()
    {
        for (int x = 0; x < blocker.Length; x++)
        {
            if (originalColor[x] == Color.red)
            {
                blockerSprite[x].color = Color.white;
            }
            else
            {
                blockerSprite[x].color = Color.red;
            }

            if (originalName[x] == "NoBlink")
            {
                blocker[x].name = "Wall";
            }
            else
            {
                blocker[x].name = "NoBlink";
            }
        }
    }
}
