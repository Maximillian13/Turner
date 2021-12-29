using UnityEngine;
using System.Collections;

public class BlinkWalls : MonoBehaviour 
{
    public float rate;
    private float timer;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCol;
	// Use this for initialization
	void Start () 
    {
        timer = 0;
        sprite = this.GetComponent<SpriteRenderer>();
        boxCol = this.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (timer > rate)
        {
            this.sprite.enabled = !this.sprite.enabled;
            this.boxCol.enabled = !this.boxCol.enabled;
            timer = 0;
        }
        timer += Time.deltaTime;
	}
}
