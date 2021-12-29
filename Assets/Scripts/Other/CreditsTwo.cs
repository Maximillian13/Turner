// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsTwo : MonoBehaviour
{

    public Rigidbody2D backgroundMover;
    public Image fadeOut;
    private float oldTime;
    private float oldTimeTwo;
    private float oldTimeThree;
    private float alpha;
    private float speed = .6f;
    private float maxSpeed = .4f;
    private float escapeTimer;

    void Awake()
    {
        fadeOut.color = new Color(0, 0, 0, 0);
        alpha = 0;
        oldTimeThree = Time.time;
    }

    void Start()
    {
        oldTime = Time.time;
    }

    void FixedUpdate()
    {
        escapeTimer += Time.deltaTime;       

        if (backgroundMover.transform.position.y > -23)
        {
            // Move the background
            if (Time.time >= oldTime + 2)
            {
                if (backgroundMover.velocity.magnitude <= maxSpeed)
                {
                    backgroundMover.AddForce(transform.up * -speed);
                }
            }
            oldTimeTwo = Time.time;
        }
        else
        {
            backgroundMover.velocity = new Vector2(0, 0);
            if(Time.time > oldTimeTwo + 1)
            {
                fadeOut.color = new Color(225, 225, 225, alpha);
                alpha += .0025f;
            }

        }

        // esc Key
        if (Input.GetButtonDown("Pause") && escapeTimer > 2)
        {
            //SceneManager.LoadScene(0);
            backgroundMover.transform.position = new Vector3(0, -23);
        }
        if (fadeOut.color.a >= 1 && Time.time > oldTimeThree + 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
