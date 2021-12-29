// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;

public class ScrollCredits : MonoBehaviour 
{

    public float xDif;

    private Vector3 velocity = Vector3.zero;
    private Transform CreditRunner;
    private Camera Maincamera;

    void Start()
    {
        // Gets the player
        CreditRunner = GameObject.FindWithTag("CreditRunner").transform;
        Maincamera = GetComponent<Camera>();
    }

    void Update()
    {
        // If it can find the player then do this
        if (CreditRunner == true)
        {
            Vector3 point = Maincamera.WorldToViewportPoint(CreditRunner.position);
            Vector3 delta = CreditRunner.position - Maincamera.ViewportToWorldPoint(new Vector3(0.5f - xDif, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0);
        }
    }
}
