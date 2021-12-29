using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TranslatePagePickUp : MonoBehaviour 
{
    public Sprite[] pageSprite;
    private Image pageImg;
    private string lang;
    private GameObject pressEnterGO;
    private Text pressEnter;

	// Use this for initialization
	void Start () 
    {
        pageImg = this.GetComponent<Image>();

        if (SteamManager.Initialized)
        {
            lang = Steamworks.SteamUtils.GetSteamUILanguage();
        }
        else
        {
            lang = "english";
        }

        if(lang == "spanish")
        {
			pageImg.sprite = pageSprite[1];
			pressEnterGO = GameObject.Find("PressEnter");
			if (pressEnterGO != null)
			{
				pressEnter = pressEnterGO.GetComponent<Text>();
				pressEnter.text = "Pulse Intro para continuar...";
			}
        }
        else
        {
			pageImg.sprite = pageSprite[0];
		}
	}
}
