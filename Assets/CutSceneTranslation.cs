using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CutSceneTranslation : MonoBehaviour 
{
    private string language;
    private Text pressAnyText;
    private Text holdEscapeText;

    void Start()
    {
        if (GameObject.Find("Hold escape") != null && GameObject.Find("Press any button to continue") != null)
        {
            holdEscapeText = GameObject.Find("Hold escape").GetComponent<Text>();
            pressAnyText = GameObject.Find("Press any button to continue").GetComponent<Text>();
        }

        if (SteamManager.Initialized)
        {
            language = Steamworks.SteamUtils.GetSteamUILanguage();
        }
        else
        {
            language = "english";
        }

        // Translated
        if (language.Equals("spanish"))
        {
            if (holdEscapeText != null && pressAnyText != null)
            {
                holdEscapeText.text = "Presione escape pare brincar";
                pressAnyText.text = "Presione qualcier boton para continuar...";
            }
        }

    }
}
