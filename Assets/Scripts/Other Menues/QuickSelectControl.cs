// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuickSelectControl : MonoBehaviour 
{
    public InputField inputField;
    public Text inputFieldText;
    private GameObject eventSystemArgus;
    private GameObject backButton;
	// Use this for initialization
	void Start () 
    {
        eventSystemArgus = GameObject.Find("EventSystem");
        backButton = GameObject.Find("Back");

	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("MenuVert") < 0)
        {
            eventSystemArgus.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(backButton);
        }
        
        if (inputField.isFocused == false)
        {
            inputField.text = "";
        }
    }
}
