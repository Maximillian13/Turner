using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FilterColor : MonoBehaviour 
{

    private Image filter;
    private float alpha;
    private bool up;
    private bool down;
    private bool useFilter;
	// Use this for initialization
	void Start () 
    {
        alpha = .05f;
        filter = this.GetComponent<Image>();
        filter.color = new Color(100, 0, 0, alpha);
        up = true;
        down = false;

        if(PlayerPrefs.GetInt("VIN") == 0)
        {
            useFilter = true;
            filter.gameObject.SetActive(true);
        }
        else
        {
            useFilter = false;
            filter.gameObject.SetActive(false);
        }
	}

    void FixedUpdate()
    {
        if (useFilter == true)
        {
            if (alpha <= .05f)
            {
                up = true;
                down = false;
            }
            if (alpha >= .2f)
            {
                up = false;
                down = true;
            }
            if (up == true)
            {
                alpha += .001f;
            }
            if (down == true)
            {
                alpha -= .001f;
            }
            filter.color = new Color(100, 0, 0, alpha);
        }
    }
}
