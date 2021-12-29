using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Steamworks;

public class SpeedRunRecorder : MonoBehaviour 
{
    // Leader board
    CallResult<LeaderboardFindResult_t> callResult;
    CallResult<LeaderboardScoreUploaded_t> callResult2;

    private Text speedRunRecordedTimeTextBox;

    private int totalTimeHours;
    private int totalTimeMinutes;
    private int totalTimeSeconds;

    private int totalTime;

    private string lang;

	// Use this for initialization
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


        speedRunRecordedTimeTextBox = GameObject.Find("RecordedTime").GetComponent<Text>();
        if (PlayerPrefs.GetInt("SPEEDRUN") == 0)
        {
            speedRunRecordedTimeTextBox.gameObject.SetActive(false);
        }
        else
        {
            speedRunRecordedTimeTextBox.gameObject.SetActive(true);

            totalTime = PlayerPrefs.GetInt("TOTALTIME");

            totalTimeHours = (int)totalTime / 3600;
            totalTimeMinutes = (int)totalTime / 60;
            totalTimeSeconds = (int)totalTime % 60;

            // Credits one
            if(SceneManager.GetActiveScene().buildIndex == 435)
            {
                if (lang.Equals("spanish"))
                {
					speedRunRecordedTimeTextBox.text = string.Format("Tiempo de base juego {0}:{1: 00}:{2: 00} \nSigue jugando para una carrera completa de juego. ", totalTimeHours, totalTimeMinutes % 60, totalTimeSeconds);
                }
                else
                {
					speedRunRecordedTimeTextBox.text = string.Format("Base Game Time {0}:{1: 00}:{2: 00} \nKeep playing for a full game speedrun. ", totalTimeHours, totalTimeMinutes % 60, totalTimeSeconds);
				}
				// Leader board
				callResult = CallResult<LeaderboardFindResult_t>.Create(OnFindLeaderboard);
                callResult.Set(SteamUserStats.FindLeaderboard(("Speedrun_Base")));
            }
            else // Credits two
            {
                if (lang.Equals("spanish"))
                {
                    speedRunRecordedTimeTextBox.text = string.Format("Tiempo de juego completo {0}:{1: 00}:{2: 00} ", totalTimeHours, totalTimeMinutes % 60, totalTimeSeconds);
				}
				else
                {
					speedRunRecordedTimeTextBox.text = string.Format("Full Game Time {0}:{1: 00}:{2: 00} ", totalTimeHours, totalTimeMinutes % 60, totalTimeSeconds);
				}
				// Leader board
				callResult = CallResult<LeaderboardFindResult_t>.Create(OnFindLeaderboard);
                callResult.Set(SteamUserStats.FindLeaderboard(("Speedrun_Full")));
            }
            
        }
	}

    // Leader board
    #region LeaderBoard
    void Update()
    {
        SteamAPI.RunCallbacks();
    }

    private void OnFindLeaderboard(LeaderboardFindResult_t pCallback, bool bIOFailure)
    {
        if (!bIOFailure && pCallback.m_bLeaderboardFound != 0)
        {
            callResult = CallResult<LeaderboardFindResult_t>.Create(OnFindLeaderboard);

            SteamLeaderboard_t leaderboard = pCallback.m_hSteamLeaderboard; // the leaderboard handle

            // the following is basically another call within a call...
            // compare the next two lines with the two code lines at the beginning of my post
            callResult2 = CallResult<LeaderboardScoreUploaded_t>.Create(OnScoreUploaded);
            callResult2.Set(SteamUserStats.UploadLeaderboardScore(leaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, totalTime, null, 0));
        }
    }

    private void OnScoreUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure)
    {
        // you can investigate here if this method is triggered and if the score was submitted.
        // check pCallback.m_bSuccess or bIOFailure etc.
    }
    #endregion
}
