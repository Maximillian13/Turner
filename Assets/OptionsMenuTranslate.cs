using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionsMenuTranslate : MonoBehaviour {

    public Text resolutionText;
    public Text graphicQualityText;
    public Text fullScreenText;
    public Text vignetteText;
    public Text musicVolumeText;
    public Text backText;
    public Text deleteAllProgressText;
    public Text applyText;
    public Text delDiag;
    public Text yesText;
    public Dropdown graphicQualityDropDown;

    private string language;

    void Start()
    {
        if (SteamManager.Initialized)
        {
            language = Steamworks.SteamUtils.GetSteamUILanguage();
        }
        else
        {
            language = "english";
        }

        if (language.Equals("spanish"))
        {
            resolutionText.text = "Resoluciones";
            graphicQualityText.text = "Calidad Grafica";
            fullScreenText.text = "Volumen De Musica";
            musicVolumeText.text = "Pantalla Entera";
            vignetteText.text = "Borde Oscuro";
            backText.text = "Regresar";
            deleteAllProgressText.text = "Borra Todo El Progreso";
            applyText.text = "Aplica";
            delDiag.text = "Estas seguro que quieres borrar todo el progresso?";
            yesText.text = "Si";

            List<string> spanishOptions = new List<string>();
            spanishOptions.Add("Alto");
            spanishOptions.Add("bajo");

            graphicQualityDropDown.ClearOptions();
            graphicQualityDropDown.AddOptions(spanishOptions);
        }

    }
}
