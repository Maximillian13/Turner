using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Steamworks;

public class MainMenuTranslate : MonoBehaviour 
{
    public Text levelSelect;
    public Text pages;
    public Text options;
    public Text quit;
    public Text theTrial;
    public Text speedRun;
    public Text quitDiag;
    public Text speedrunDiag;
    public Text yes;
    public Text ok;

    private string language;

	void Start () 
    {
        if (SteamManager.Initialized)
        {
            language = SteamUtils.GetSteamUILanguage();
        }
        else
        {
            language = "english";
        }

        // Translation Done
        if(language.Equals("spanish"))
        {
            levelSelect.text = "Nivel Seleccionado";
            pages.text = "Paginas";
            options.text = "Opciones";
            quit.text = "Salir";
            theTrial.text = "El Sendero";
            speedRun.text = "Carrera De Velocidad";
            quitDiag.text = "Seguro que quieres salir";
            speedrunDiag.text = "No puedes resumir de qualquier punto, tienes que terminar el juego en un corrida, para que su nombre sea puesto en la tabla de lideres.";
            yes.text = "Si";
            ok.text = "Bueno";
        }

	}

}
