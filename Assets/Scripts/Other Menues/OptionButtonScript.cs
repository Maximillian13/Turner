// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionButtonScript : MonoBehaviour 
{
    public Dropdown resolutionDropDown;
    public Dropdown qualityDropDown;
    public Toggle checkBox;
    public Toggle vinBox;
    //public Toggle speedBox;
    public Slider volumeSlider;
    private GameObject musicGO;
    private AudioSource music;

    void Awake()
    {
        // Mess with music sliders
        musicGO = GameObject.Find("MainMenuMusic");
        music = musicGO.GetComponent<AudioSource>();
        volumeSlider.value = PlayerPrefs.GetFloat("MUSICVOLUME");
    }
    void Start()
    {
        //Set delete progress to false so you dont delete by accented 
        // Change quality
        if (QualitySettings.GetQualityLevel() == 0)
        {
            // Low
            qualityDropDown.value = 1;
        }
        else
        {
            // High
            qualityDropDown.value = 0;
        }

        if (Screen.currentResolution.width == 1600 && Screen.currentResolution.height == 900)
        {
            resolutionDropDown.value = 1;
        }
        else if (Screen.currentResolution.width == 1366 && Screen.currentResolution.height == 768)
        {
            resolutionDropDown.value = 2;
        }
        else if (Screen.currentResolution.width == 1360 && Screen.currentResolution.height == 768)
        {
            resolutionDropDown.value = 3;
        }
        else if (Screen.currentResolution.width == 1280 && Screen.currentResolution.height == 720)
        {
            resolutionDropDown.value = 4;
        }
        else
        {
            resolutionDropDown.value = 0;
        }

        // Vin
        if (PlayerPrefs.GetInt("VIN") == 0)
        {
            vinBox.isOn = true;
        }
        else
        {
            vinBox.isOn = false;
        }

        // SpeedRun
        //if (PlayerPrefs.GetInt("SPEEDRUN") == 0)
        //{
        //    speedBox.isOn = false;
        //}
        //else
        //{
        //    speedBox.isOn = true;
        //}

        if (PlayerPrefs.GetInt("BEATGAME") == 1)
        {
            vinBox.gameObject.SetActive(true);
        }
        else
        {
            vinBox.gameObject.SetActive(false);
        }

    }

    public void LoadAnyLevel(int level)
    {
        SceneManager.LoadScene(level);
    }


    public void Apply()
    {
        PlayerPrefs.SetFloat("MUSICVOLUME", volumeSlider.value);
        music.volume = volumeSlider.value;
        PlayerPrefs.SetInt("MUSICGOOD", 1);

        // Change quality
        if (qualityDropDown.value == 0)
        {
            // Low
            QualitySettings.SetQualityLevel(1);
        }
        else
        {
            // High
            QualitySettings.SetQualityLevel(0);
        }

        // The resolution on this ding dong is sac!
        if (resolutionDropDown.value == 0)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (resolutionDropDown.value == 1)
        {
            Screen.SetResolution(1600, 900, true);
        }
        else if (resolutionDropDown.value == 2)
        {
            Screen.SetResolution(1366, 768, true);
        }
        else if (resolutionDropDown.value == 3)
        {
            Screen.SetResolution(1360, 768, true);
        }
        else
        {
            Screen.SetResolution(1280, 720, true);
        }

        // Full scrim
        if (checkBox.isOn == true)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }

        // Vin
        if(vinBox.isOn == true)
        {
            PlayerPrefs.SetInt("VIN", 0);
        }
        else
        {
            PlayerPrefs.SetInt("VIN", 1);
        }

        // Speed Run
        //if (speedBox.isOn == true)
        //{
        //    PlayerPrefs.SetInt("SPEEDRUN", 1);
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("SPEEDRUN", 0);
        //}
    }
}
