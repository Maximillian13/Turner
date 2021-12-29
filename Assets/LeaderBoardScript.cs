using UnityEngine;
using System.Collections;
using Steamworks;

public class LeaderBoardScript : MonoBehaviour
{
    CallResult<LeaderboardFindResult_t> callResult;
    CallResult<LeaderboardScoreUploaded_t> callResult2;

    void Start()
    {
        callResult = CallResult<LeaderboardFindResult_t>.Create(OnFindLeaderboard);
        callResult.Set(SteamUserStats.FindLeaderboard(("Speedrun_Base")));
    }

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
            callResult2.Set(SteamUserStats.UploadLeaderboardScore(leaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, 5555, null, 0));
        }
    }

    private void OnScoreUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure)
    {
        // you can investigate here if this method is triggered and if the score was submitted.
        // check pCallback.m_bSuccess or bIOFailure etc.
    }
}
