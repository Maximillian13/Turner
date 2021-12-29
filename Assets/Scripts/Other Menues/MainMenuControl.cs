// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuControl : MonoBehaviour 
{
    // Fields
    public Button[] buttons;
    public Text lableSpeedRun;
    public Button ok;
    public Button btnLevelSelect;
    public Button btnPage;
    public Button btnSpeedrun;
    public Button btnTurnersTrails;
    public Text textNewCont;

    private string lang;

	// Sets the correct buttons off or on
	void Awake () 
    {
        if(SteamManager.Initialized)
        {
            lang = Steamworks.SteamUtils.GetSteamUILanguage();
        }

        // Translated
	    if (PlayerPrefs.GetInt("LEVEL") == 0)
        {
            if (lang.Equals("spanish"))
            {
				textNewCont.text = "Juego Nuevo";
            }
            else
            {
				textNewCont.text = "Juego Nuevo";
			}
			btnLevelSelect.interactable = false;
            btnPage.interactable = false;
        }
        else if (PlayerPrefs.GetInt("LEVEL") == 209)
        {
            if (lang.Equals("spanish"))
            {
                textNewCont.text = "Sigue Trabajando";
			}
			else
            {
                textNewCont.text = "Keep Working";
			}
			btnLevelSelect.interactable = true;
            btnPage.interactable = true;
        }
        else
        {
            if (lang.Equals("spanish"))
            {
				textNewCont.text = "Continua";
            }
            else
            {
				textNewCont.text = "Continue";
			}
			btnLevelSelect.interactable = true;
            btnPage.interactable = true;
        }

        if (PlayerPrefs.GetInt("BEATGAME") == 1)
        {
            PlayerPrefs.SetInt("SPEEDRUNGOOD", 1);
        }

        if(PlayerPrefs.GetInt("SPEEDRUNGOOD") == 1)
        {
            btnSpeedrun.gameObject.SetActive(true);
            btnTurnersTrails.gameObject.SetActive(true);
        }
        else
        {
            btnSpeedrun.gameObject.SetActive(false);
            btnTurnersTrails.gameObject.SetActive(false);
        }
	}

    // Makes sure the levels wont send you back to the level select screen
    void Start()
    {
        PlayerPrefs.SetInt("THISLEVELONLY", 0);
        PlayerPrefs.SetInt("WHATLEVELONLEVELSELECT", 0);
    }

    public void StartGame()
    {
        if (PlayerPrefs.GetInt("LEVEL") == 0)
        {
            SceneManager.LoadScene(415);
        }
        else if (PlayerPrefs.GetInt("LEVEL") == 209 && PlayerPrefs.GetInt("BEATGAME") == 1)
        {
            SceneManager.LoadScene(425);
        }
        else
        {
            // When continuing game
            GameObject music = GameObject.FindWithTag("Music");
            if (music != null)
            {
                Destroy(music);
            }
            SceneManager.LoadScene(PlayerPrefs.GetInt("LEVEL"));
        }
    }

    public void ShowDiag()
    {
        for (int x = 0; x < buttons.Length; x++)
        {
            buttons[x].enabled = false;
        }
        ok.gameObject.SetActive(true);
        lableSpeedRun.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(ok.gameObject);
    }

    public void StartSpeedrun()
    {
        PlayerPrefs.SetInt("SPEEDRUN", 1);
        SceneManager.LoadScene(415);
    }
}
