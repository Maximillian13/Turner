using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResetShower : MonoBehaviour {

    private Image restart;
    private float timer;
    
	// Use this for initialization
	void Start () 
    {
        restart = this.gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
	    
        if(timer < 1)
        {
            restart.color = Color.white;
        }
        else if(timer > 1 && timer < 2)
        {
            restart.color = Color.grey;
        }
        else
        {
            timer = 0;
        }

	}
}
