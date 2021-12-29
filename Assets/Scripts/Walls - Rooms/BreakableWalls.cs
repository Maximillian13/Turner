// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class BreakableWalls : MonoBehaviour 
{
    // Fields 
    private GameObject black;
    private float oldTime;
    private int flicker;
    private bool onOff;
    private bool alreadyIn;
    private bool dontGoThrough;
    private SpriteRenderer sprite;
    private BoxCollider2D[] boxCol;
    public Rigidbody2D[] brokenGlass;

    void Start()
    {
        if (this.transform.childCount > 0 && this.transform.GetChild(0).name != "Gib")
        {
            black = this.transform.GetChild(0).gameObject;
            black.gameObject.SetActive(false);
        }
        oldTime = Mathf.Infinity;
        alreadyIn = false;
        dontGoThrough = false;
        flicker = 0;
        sprite = GetComponent<SpriteRenderer>();
        //boxCol = GetComponent<BoxCollider2D>();
        boxCol = GetComponents<BoxCollider2D>();
        for (int x = 0; x < brokenGlass.Length; x++)
        {
            brokenGlass[x].gameObject.SetActive(false);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player" || other.tag == "Box" || other.tag == "Circle") && alreadyIn == false)
        {
            oldTime = Time.time + 1;
            alreadyIn = true;
        }
    }

    // Makes the breakable block go away and make it flicker
    void FixedUpdate()
    {
        if (Time.timeScale == 1)
        {
            if (oldTime - .75f <= Time.time && dontGoThrough == false)
            {
                if (flicker % 7 == 0)
                {
                    onOff = !onOff;
                }
                sprite.enabled = onOff;
                flicker++;
            }
        }
        if (oldTime <= Time.time && dontGoThrough == false)
        {
            if (black != null)
            {
                black.gameObject.SetActive(true);
                this.boxCol[0].enabled = false;
                this.boxCol[1].enabled = false;
                Gib();
                this.sprite.enabled = false;
            }
            else
            {
                Gib();
                Destroy(this.gameObject);
            }
            dontGoThrough = true;
        }
    }

    private void Gib()
    {
        for (int x = 0; x < brokenGlass.Length; x++)
        {
            brokenGlass[x].gameObject.SetActive(true);
            brokenGlass[x].transform.parent = null;
            brokenGlass[x].AddForce(new Vector2(0,-100));
            brokenGlass[x].AddTorque(Random.Range(-200, 201));
        }
    }
}
