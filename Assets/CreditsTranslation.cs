using UnityEngine;
using System.Collections;

public class CreditsTranslation : MonoBehaviour {

    public SpriteRenderer[] words;
    public Sprite[] translatedWords;

    private string lang;
	
	void Start () 
    {
        if (SteamManager.Initialized)
        {
            lang = Steamworks.SteamUtils.GetSteamUILanguage();
        }
        else
        {
            lang = "english";
        }

        if(lang.Equals("spanish"))
        {
            for(int x = 0; x < words.Length; x++)
            {
                words[x].sprite = translatedWords[x];
            }
        }
	}

}
