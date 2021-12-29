using UnityEngine;
using System.Collections;

public class BreakGlass : MonoBehaviour 
{
    public float breakPower;
    public Rigidbody2D[] brokenGlass;
    private Rigidbody2D ridg;
    private float fastedSpeed;

    void Start()
    {
        fastedSpeed = 0;
        if (breakPower == 0)
        {
            breakPower = 8;
        }
        for (int x = 0; x < brokenGlass.Length; x++)
        {
            brokenGlass[x].gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Box" || other.gameObject.tag == "Circle") && other.name != "Cling")
        {
            ridg = other.GetComponent<Rigidbody2D>();
            if(Mathf.Abs(ridg.velocity.x) > Mathf.Abs(ridg.velocity.y))
            {
                fastedSpeed = Mathf.Abs(ridg.velocity.x);
            }
            else
            {
                fastedSpeed = Mathf.Abs(ridg.velocity.y);
            }
            if (Mathf.Abs(fastedSpeed) > Mathf.Abs(breakPower))
            {
                for (int x = 0; x < brokenGlass.Length; x++)
                {
                    brokenGlass[x].gameObject.SetActive(true);
                    brokenGlass[x].transform.parent = null;
                    brokenGlass[x].AddForce(ridg.velocity * 25);
                    brokenGlass[x].AddTorque(Random.Range(-200, 201));
                }
                // Make it look like its crashing through
                ridg.AddForce(new Vector2(-ridg.velocity.x * 25, -ridg.velocity.y * 25));
                Destroy(this.gameObject);
            }
        }
    }
}
