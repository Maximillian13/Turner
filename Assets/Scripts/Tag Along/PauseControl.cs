// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Steamworks;

public class PauseControl : MonoBehaviour 
{
    // Fields
    public Image pageBehind;
    public Text lblVolume;
    public Slider sldVolume;
    public Button btnResume;
    public Button btnMainMenu;
    private Text levelNumber;
    private Image deathFade;
    private Image filter;
    private float escapeTimer;

    private Text resumeText;
    private Text mainMenuText;
    private string lang;
    
    // Steam API
    protected Callback<GameOverlayActivated_t> gameOverlayActivated;

    private void OnEnable()
    {
        if (SteamManager.Initialized == true)
        {
            gameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverLayActivated);
        }
    }

    // Hides all
    void Start()
    {
        resumeText = GameObject.Find("Resume").GetComponentInChildren<Text>();
        mainMenuText = GameObject.Find("Main Menu").GetComponentInChildren<Text>();


        GameObject levelOnGO = GameObject.Find("LevelNumber");
        levelNumber = levelOnGO.GetComponent<Text>();
        levelNumber.gameObject.SetActive(false);
        int sceenNumber = SceneManager.GetActiveScene().buildIndex - 4;

        if (SteamManager.Initialized)
        {
            lang = Steamworks.SteamUtils.GetSteamUILanguage();
        }
        else
        {
            lang = "english";
        }

        // Translated
        if (lang.Equals("spanish"))
        { 
            resumeText.text = "Curriculum";
            mainMenuText.text = "Menu Principal";
            lblVolume.text = "Volumen";
            if (sceenNumber < 439)
            {
                levelNumber.text = "Nivel: " + sceenNumber;
            }
            else
            {
                levelNumber.text = "Nivel Especial: " + (sceenNumber - 438);
            }
        }
        else
        {
            if (sceenNumber < 439)
            {
                levelNumber.text = "Level: " + sceenNumber;
            }
            else
            {
                levelNumber.text = "Special Level: " + (sceenNumber - 438);
            }
        }


        pageBehind.gameObject.SetActive(false);
        lblVolume.gameObject.SetActive(false);
        sldVolume.gameObject.SetActive(false);
        btnMainMenu.gameObject.SetActive(false);
        btnResume.gameObject.SetActive(false);

        GameObject deathFadeGO = GameObject.Find("DeathFade");
        deathFade = deathFadeGO.GetComponent<Image>();
        GameObject filterGO = GameObject.Find("Filter");

        sldVolume.value = PlayerPrefs.GetFloat("MUSICVOLUME");

        if (filterGO != false)
        {
            filter = filterGO.GetComponent<Image>();
            filter.transform.SetAsFirstSibling();
        }
        deathFade.transform.SetAsFirstSibling();
        //btnResume.transform.SetSiblingIndex(0);
        //btnMainMenu.transform.SetSiblingIndex(1);
        //eventSystemArgus = GameObject.Find("EventSystem");
        //resumeButton = GameObject.Find("Resume");
    }

    void Update()
    {
        escapeTimer += Time.deltaTime;
        //eventSystemArgus.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(resumeButton);
        // Esc key 
        if (Input.GetButtonDown("Pause"))
        {
            // if its the level after the cut scene 
            if (SceneManager.GetSceneAt(0).buildIndex == 5 || SceneManager.GetSceneAt(0).buildIndex == 55 || SceneManager.GetSceneAt(0).buildIndex == 105 || SceneManager.GetSceneAt(0).buildIndex == 155 || SceneManager.GetSceneAt(0).buildIndex == 194 || SceneManager.GetSceneAt(0).buildIndex == 205 || SceneManager.GetSceneAt(0).buildIndex == 210 || SceneManager.GetSceneAt(0).buildIndex == 260 || SceneManager.GetSceneAt(0).buildIndex == 310 || SceneManager.GetSceneAt(0).buildIndex == 360 || SceneManager.GetSceneAt(0).buildIndex == 399 || SceneManager.GetSceneAt(0).buildIndex == 410)
            {
                if (escapeTimer > .75f)
                {
                    // Sets the game to be paused and shows buttons, else set the game to resume and hide buttons
                    if (Time.timeScale == 1)
                    {
                        Pause();
                    }
                    else
                    {
                        Resume();
                    }
                }
            }
            else
            {
                // Sets the game to be paused and shows buttons, else set the game to resume and hide buttons
                if (Time.timeScale == 1)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }
    }

    // Resumes the game
    public void Resume()
    {
        Time.timeScale = 1;
        pageBehind.gameObject.SetActive(false);
        lblVolume.gameObject.SetActive(false);
        sldVolume.gameObject.SetActive(false);
        levelNumber.gameObject.SetActive(false);
        btnMainMenu.gameObject.SetActive(false);
        btnResume.gameObject.SetActive(false);
    }

    // Pauses the game
    private void Pause()
    {
        Time.timeScale = 0;
        pageBehind.gameObject.SetActive(true);
        lblVolume.gameObject.SetActive(true);
        sldVolume.gameObject.SetActive(true);
        levelNumber.gameObject.SetActive(true);
        btnMainMenu.gameObject.SetActive(true);
        btnResume.gameObject.SetActive(true);
    }

    // Goes back to the main menu
    public void MainMenu()
    {
        Time.timeScale = 1;
        GameObject music = GameObject.FindWithTag("Music");
        if (music != null)
        {
            Destroy(music);
        }
        SceneManager.LoadScene(0);
    }

    public void ChangeMusicVolume()
    {
        PlayerPrefs.SetFloat("MUSICVOLUME", sldVolume.value);
        PlayerPrefs.SetInt("MUSICGOOD", 1);
    }

    private void OnGameOverLayActivated(GameOverlayActivated_t callBack)
    {
        if (callBack.m_bActive != 0)
        {
            Debug.Log("Steam overlay active");
            Pause();

        }
        else
        {
            Debug.Log("Steam overlay closed");
            Resume();
        }
    }
}
