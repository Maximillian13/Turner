using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Steamworks;

public class LPAchivments : MonoBehaviour {

    public bool conviction;
    public bool guilty;
    public bool disappointment;

	// Use this for initialization
    void Update() 
    {
        if (conviction == true) // For beating the base level pack.
        {
            if (SteamManager.Initialized)
            {
                UnlockAchievement(m_Achievements[10]);
                SteamUserStats.StoreStats();
            }
        }

        if (guilty == true) // For beating the full level pack.
        {
            if (SteamManager.Initialized)
            {
                UnlockAchievement(m_Achievements[11]);
                SteamUserStats.StoreStats();
            }
        }

        if (disappointment == true) // For finding the secret ending.
        {
            if (SteamManager.Initialized)
            {
                UnlockAchievement(m_Achievements[12]);
                SteamUserStats.StoreStats();
            }
        }
        SteamUserStats.StoreStats();
	}


    #region Achiv
    private enum Achievement : int
    {
        DISCOVERY,
        TRIALS,
        BECOMING,
        COMFORT,
        FAILURE,
        RETRY,
        SUCCESS,
        COLLECTOR,
        REALIZATION,
        CONFUSION,
        CONVICTION,
        GUILTY,
        DISAPPOINTMENT
    };

    private Achievement_t[] m_Achievements = new Achievement_t[] 
    {
        new Achievement_t(Achievement.DISCOVERY, "Discovery", "Complete Chapter 1."),
        new Achievement_t(Achievement.TRIALS, "Trials", "Complete Chapter 2."),
        new Achievement_t(Achievement.BECOMING, "Becoming", "Complete Chapter 3."),
        new Achievement_t(Achievement.COMFORT, "Comfort", "Complete Chapter 4."),
        new Achievement_t(Achievement.FAILURE, "Failure", "Complete the game."),
        new Achievement_t(Achievement.RETRY, "Retry", "Keep working."),
        new Achievement_t(Achievement.SUCCESS, "Success?", "Complete the full game."),
        new Achievement_t(Achievement.COLLECTOR, "Collector", "Collect all the pages."),
        new Achievement_t(Achievement.REALIZATION, "Realization", "Uncover your past."),
        new Achievement_t(Achievement.CONFUSION, "Confusion", "Explore room 95."),
        new Achievement_t(Achievement.CONVICTION, "Conviction", "Complete Turner's Trial."),
        new Achievement_t(Achievement.GUILTY, "Guilty", "Complete Turner's Trial all the way."),
        new Achievement_t(Achievement.DISAPPOINTMENT, "Disappointment", "Nothing has Changed.")
    };



    //-----------------------------------------------------------------------------
    // Purpose: Unlock this achievement
    //-----------------------------------------------------------------------------
    private void UnlockAchievement(Achievement_t achievement)
    {
        achievement.m_bAchieved = true;

        // mark it down
        SteamUserStats.SetAchievement(achievement.m_eAchievementID.ToString());
    }


    private class Achievement_t
    {
        public Achievement m_eAchievementID;
        public string m_strName;
        public string m_strDescription;
        public bool m_bAchieved;

        /// <summary>
        /// Creates an Achievement. You must also mirror the data provided here in https://partner.steamgames.com/apps/achievements/yourappid
        /// </summary>
        /// <param name="achievement">The "API Name Progress Stat" used to uniquely identify the achievement.</param>
        /// <param name="name">The "Display Name" that will be shown to players in game and on the Steam Community.</param>
        /// <param name="desc">The "Description" that will be shown to players in game and on the Steam Community.</param>
        public Achievement_t(Achievement achievementID, string name, string desc)
        {
            m_eAchievementID = achievementID;
            m_strName = name;
            m_strDescription = desc;
            m_bAchieved = false;
        }
    }
    #endregion
}
