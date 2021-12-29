using UnityEngine;
using System.Collections;

public class PopUpHallucanicion : MonoBehaviour {

    public GameObject hulTurner;
    
    void Start()
    {
        hulTurner.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            hulTurner.gameObject.SetActive(true);
            Debug.Log(23);
        }
    }
}
