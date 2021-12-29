// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class BoxButtonRotate : MonoBehaviour
{
    // Public 
    public GameObject room;
    public float roomSpeed;
    public Sprite[] buttonUpDown = new Sprite[2];
    // Private 
    private SpriteRenderer buttonSprite;
    private bool goThroughAgian;
    private string tagOfWhatsIn;
    private bool rotateOn;
    private bool activeButton;

    void Start()
    {
        // Set all the stuff correct
        activeButton = true;
        rotateOn = false;
        tagOfWhatsIn = null;
        buttonSprite = this.GetComponent<SpriteRenderer>();
        buttonSprite.sprite = buttonUpDown[0];
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // So the button does not get turned off and on while standing on it and something else touches it
        if (tagOfWhatsIn == null || tagOfWhatsIn == other.tag)
        {
            if ((other.tag == "Box" || other.tag == "Player" || other.tag == "Circle") && activeButton == true)
            {
                rotateOn = true;
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
                rotateOn = false;
                buttonSprite.sprite = buttonUpDown[0];
                tagOfWhatsIn = null;
                goThroughAgian = true;
            }
        }
    }


    void FixedUpdate()
    {
        if(rotateOn == true)
        {
            room.transform.Rotate(new Vector3(0, 0, roomSpeed));
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
                    rotateOn = true;
                    buttonSprite.sprite = buttonUpDown[1];
                    tagOfWhatsIn = other.tag;
                }
                else
                {
                    rotateOn = false;
                }
            }
            goThroughAgian = false;
        }
    }
}
