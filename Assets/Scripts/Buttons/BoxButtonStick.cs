// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class BoxButtonStick : MonoBehaviour
{
    // Public
    public GameObject[] blocker = new GameObject[0];
    public Sprite[] buttonUpDown = new Sprite[2];
    // Private
    private SpriteRenderer buttonSprite;
    private bool activeButton;

    void Start()
    {
        // Set things to default
        activeButton = true;
        buttonSprite = this.GetComponent<SpriteRenderer>();
        buttonSprite.sprite = buttonUpDown[0];
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Activate the button
        if ((other.tag == "Box" || other.tag == "Player" || other.tag == "Circle") && activeButton == true)
        {
            for (int x = 0; x < blocker.Length; x++)
            {
                blocker[x].SetActive(!blocker[x].activeSelf);
            }
            buttonSprite.sprite = buttonUpDown[1];
            activeButton = false;

        }
    }

}
