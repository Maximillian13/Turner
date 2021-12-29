using UnityEngine;
using System.Collections;

public class OutsideDeath : MonoBehaviour
{
    private SpriteRenderer sprite;
    private int counter = 0;
	// Use this for initialization
	void Start ()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();

	}

    void FixedUpdate()
    {
        if(counter % 15 == 0)
        {
            sprite.color = new Color(Random.Range(.85f,1), 0, 0);
        }
        counter++;
    }

    // Resets the level
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerControlsStart.killPlayer = true;
            PlayerControls.killPlayer = true;
            PlayerControlsDoubleJump.killPlayer = true;
            PlayerControlsCling.killPlayer = true;
            PlayerControlsBlink.killPlayer = true;
            PlayerControlsDisoriented.killPlayer = true;
            PlayerControlsEnd.killPlayer = true;
        }
    }
}
