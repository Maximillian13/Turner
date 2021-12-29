using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeedRun : MonoBehaviour 
{
    private Text speedRunTotalTimeTextBox;
    private Text speedRunLevelTimeTextBox;
    private Text speedRunPrevTimeTextBox;

    private float totalTime;

    private int totalTimeHours;
    private int totalTimeMinutes;
    private int totalTimeSeconds;

    private int levelTimeMinutes;
    private int levelTimeSeconds;

    private int prevTimeMinutes;
    private int prevTimeSeconds;

    // Use this for initialization
    void Start()
    {
        // Get the text boxes
        speedRunTotalTimeTextBox = GameObject.Find("SpeedRunTotalTime").GetComponent<Text>();
        speedRunLevelTimeTextBox = GameObject.Find("SpeedRunLevelTime").GetComponent<Text>();
        speedRunPrevTimeTextBox = GameObject.Find("PrevTime").GetComponent<Text>();

        if (PlayerPrefs.GetInt("SPEEDRUN") == 0)
        {
            speedRunTotalTimeTextBox.gameObject.SetActive(false);
            speedRunLevelTimeTextBox.gameObject.SetActive(false);
            speedRunPrevTimeTextBox.gameObject.SetActive(false);
        }
        else
        {
            speedRunTotalTimeTextBox.gameObject.SetActive(true);
            speedRunLevelTimeTextBox.gameObject.SetActive(true);
            speedRunPrevTimeTextBox.gameObject.SetActive(true);

            if (PlayerPrefs.GetInt("PREVLEV") != SceneManager.GetActiveScene().buildIndex)
            {
                // Sets how long you took on the previous level
                PrevTimeControl(PlayerPrefs.GetInt("LEVELTIME"));
                PlayerPrefs.SetInt("PREVTIME", PlayerPrefs.GetInt("LEVELTIME"));
            }
            else
            {
                // Sets how long you took on the previous level
                PrevTimeControl(PlayerPrefs.GetInt("PREVTIME"));

            }
            totalTime = PlayerPrefs.GetInt("TOTALTIME");
            PlayerPrefs.SetInt("PREVLEV", SceneManager.GetActiveScene().buildIndex);
            // For the disable problem
            PlayerPrefs.SetInt("CANDISABLE", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("SPEEDRUN") == 1)
        {
            // Controls the timer for the level
            LevelTimeControl();

            // Controls the timer for the whole speed run
            TotalTimeControl();
        }
    }


    void OnDisable()
    {
        if (PlayerPrefs.GetInt("CANDISABLE") == 1)
        {
            if (PlayerPrefs.GetInt("SPEEDRUN") == 1)
            {
                PlayerPrefs.SetInt("LEVELTIME", (int)Time.timeSinceLevelLoad);
                PlayerPrefs.SetInt("TOTALTIME", (int)totalTime);
            }
            PlayerPrefs.SetInt("CANDISABLE", 0);
        }
    }


    // Controls the timer for the level
    private void LevelTimeControl()
    {
        levelTimeMinutes = (int)Time.timeSinceLevelLoad / 60;
        levelTimeSeconds = (int)Time.timeSinceLevelLoad % 60;
        speedRunLevelTimeTextBox.text = string.Format(" {0: 00}:{1: 00}", levelTimeMinutes, levelTimeSeconds);
    }

    // Controls the timer for the level
    private void TotalTimeControl()
    {
        totalTime += Time.deltaTime;
        totalTimeHours = (int)totalTime / 3600;
        totalTimeMinutes = (int)totalTime / 60;
        totalTimeSeconds = (int)totalTime % 60;
        speedRunTotalTimeTextBox.text = string.Format("{0}:{1: 00}:{2: 00} ", totalTimeHours, totalTimeMinutes % 60, totalTimeSeconds);
    }

    // Controls the timer for the previous level
    private void PrevTimeControl(int prevTime)
    {
        prevTimeMinutes = (int)prevTime / 60;
        prevTimeSeconds = (int)prevTime % 60;
        speedRunPrevTimeTextBox.text = string.Format(" {0: 00}:{1: 00}", prevTimeMinutes, prevTimeSeconds);
    }
}
