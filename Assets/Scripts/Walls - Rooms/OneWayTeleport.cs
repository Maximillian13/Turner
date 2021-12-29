using UnityEngine;
using System.Collections;

public class OneWayTeleport : MonoBehaviour {

    public Transform endTele;
    private Transform endTeleEndPoint;
    private Rigidbody2D ridg;
    //private float x;
    private float oldTime;

    void Start()
    {
        // Find the other direction
        endTeleEndPoint = endTele.GetChild(0);
        oldTime = -.05f;
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if((other.tag == "Player" || other.tag == "Box") && other.name != "Cling" && Time.time >= oldTime + .05f)
        {
            oldTime = Time.time;
            // Get Direction to launch
            Vector3 fromPosition = endTele.position;
            Vector3 toPosition = endTeleEndPoint.position;
            Vector3 direction = toPosition - fromPosition;

            // Get Components
            ridg = other.GetComponent<Rigidbody2D>();
            //x = ridg.velocity.x;
            // Change Player position to new position and make y velocity 0
            other.transform.position = endTele.position;
            Vector2 speedBefore = ridg.velocity;
            ridg.velocity = new Vector2(0, 0);

            // Add force in the right direction
            //if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            //{
                //ridg.AddForce(new Vector2((direction.x * speedBefore.x * 55), (direction.y * speedBefore.y * -55)));
            //}
            //ridg.AddForce(direction * 500);
            if (Mathf.Abs(speedBefore.x) > Mathf.Abs(speedBefore.y))
            {
                if(direction.x < 0)
                {
                    ridg.AddForce(direction * -speedBefore.x * 56);
                }
                else
                {
                    ridg.AddForce(direction * speedBefore.x * 56);
                }
                ridg.AddForce(new Vector2(0, speedBefore.y * 20));
            }
            else
            {
                if (speedBefore.y < 0)
                {
                    ridg.AddForce(direction * -speedBefore.y * 56);
                }
                else
                {
                    ridg.AddForce(direction * speedBefore.y * 56);
                }
                ridg.AddForce(new Vector2(speedBefore.x * 20, 0));
            }
            //ridg.AddForce(direction * 500);
        }
    }
}
