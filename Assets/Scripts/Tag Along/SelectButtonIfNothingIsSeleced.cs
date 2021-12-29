using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectButtonIfNothingIsSeleced : MonoBehaviour 
{
    public Button buttonToSelect;

	// Update is called once per frame
	void Update () 
    {
        if (EventSystem.current.currentSelectedGameObject == false && (Input.GetAxis("MenuHoriz") != 0 || Input.GetAxis("MenuVert") != 0))
        {
            //Debug.Log("dsad");
            EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject);
        }
	}
}
