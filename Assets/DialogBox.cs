using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour 
{
    public Button[] buttons;
    public Button yes;
    public Button no;
    public Text lable;
    public Text lableSpeedRun;
    public Toggle vinToggle;

	// Use this for initialization
	void Start () 
    {
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
        lable.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	public void PopUp() 
    {
        for(int x = 0; x < buttons.Length; x++)
        {
            buttons[x].enabled = false;
        }
        yes.gameObject.SetActive(true);
        no.gameObject.SetActive(true);
        lable.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(no.gameObject);
	}

    public void GoBack()
    {
        for (int x = 0; x < buttons.Length; x++)
        {
            buttons[x].enabled = true;
        }
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
        lable.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }

    public void Quit()
    {
        PlayerPrefs.SetInt("SPLASHSCREEN", 0);
        Application.Quit();
    }

    public void DeleteAll()
    {
        //PlayerPrefs.SetFloat("MUSICVOLUME", .2f);
        PlayerPrefs.SetInt("DISTORT", 0);
        PlayerPrefs.SetInt("LEVEL", 0);
        PlayerPrefs.SetInt("BEATGAME", 0);
        PlayerPrefs.SetInt("VIN", 0);
        PlayerPrefs.SetInt("SPEEDRUN", 0);
        PlayerPrefs.SetInt("LEVELTIME", 0);
        PlayerPrefs.SetInt("TOTALTIME", 0);
        PlayerPrefs.SetInt("LEVELSPECIAL", 0);
        PlayerPrefs.SetInt("BEATGAMESPECAIL", 0);
        PlayerPrefs.SetInt("PAGENUMBER0", 0);
        PlayerPrefs.SetInt("PAGENUMBER1", 0);
        PlayerPrefs.SetInt("PAGENUMBER2", 0);
        PlayerPrefs.SetInt("PAGENUMBER3", 0);
        PlayerPrefs.SetInt("PAGENUMBER4", 0);
        PlayerPrefs.SetInt("PAGENUMBER5", 0);
        PlayerPrefs.SetInt("PAGENUMBER6", 0);
        PlayerPrefs.SetInt("PAGENUMBER7", 0);
        PlayerPrefs.SetInt("PAGENUMBER8", 0);
        PlayerPrefs.SetInt("PAGENUMBER9", 0);
        PlayerPrefs.SetInt("PAGENUMBER10", 0);
        PlayerPrefs.SetInt("PAGENUMBER11", 0);
        PlayerPrefs.SetInt("PAGENUMBER12", 0);
        PlayerPrefs.SetInt("PAGENUMBER13", 0);
        PlayerPrefs.SetInt("PAGENUMBER14", 0);
        PlayerPrefs.SetInt("PAGENUMBER15", 0);
        PlayerPrefs.SetInt("PAGENUMBER16", 0);
        PlayerPrefs.SetInt("PAGENUMBER17", 0);
        PlayerPrefs.SetInt("PAGENUMBER18", 0);
        PlayerPrefs.SetInt("PAGENUMBER19", 0);
        PlayerPrefs.SetInt("PAGENUMBER20", 0);

        for (int x = 0; x < buttons.Length; x++)
        {
            buttons[x].enabled = true;
        }
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
        lable.gameObject.SetActive(false);
        vinToggle.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }
}
