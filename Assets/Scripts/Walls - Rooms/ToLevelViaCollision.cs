﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Steamworks;

public class ToLevelViaCollision : MonoBehaviour 
{
    public int level;
    public Image blackFade;
    public bool specail;
    public bool end;
    public bool end2;
    public bool dontKillTurner;
    private GameObject music;
    private AudioSource song;
    private GameObject turner;
    private bool levelComp;
    private float oldTime;
    private float alpha;
    private bool noAchiv;

    void Start()
    {
        turner = GameObject.FindWithTag("Player");
        if (end == true || end2 == true)
        {
            blackFade.color = new Color(255, 255, 255, 0);
            music = GameObject.Find("LastSong");
            if (music != null)
            {
                song = music.GetComponent<AudioSource>();
            }
        }
        else if (end2 == true)
        {
            blackFade.color = new Color(114, 0, 0, 0);
        }
        else
        {
            blackFade.color = new Color(0, 0, 0, 0);
        }
        levelComp = false;
        oldTime = Mathf.Infinity;
        alpha = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (dontKillTurner == false)
            {
                turner.gameObject.SetActive(false);
            }
            levelComp = true;
            oldTime = Time.time;
            // If the page was picked up set the playerpref of the page number to 1 so it will be seen in the class "PageButtonScripts"
            if (PagePickUp.pickedUp == true)
            {
                PlayerPrefs.SetInt("PAGENUMBER" + PagePickUp.pageNumberForAnyone, 1);

                // Check if all the pages have been picked up
                for (int x = 0; x < 20; x++)
                {
                    // If a page has not been found set noAchiv to true
                    if (PlayerPrefs.GetInt("PAGENUMBER" + x) == 0)
                    {
                        noAchiv = true;
                    }
                }

                // If noAchiv is still false, every page must be found, give achievement.
                if (noAchiv == false)
                {
                    if (SteamManager.Initialized)
                    {
                        UnlockAchievement(m_Achievements[7]);
                        SteamUserStats.StoreStats();
                    }
                }
            }
            // For last levels
            if(specail == true)
            {
                SceneManager.LoadScene(level);
            }
        }
    }

    void FixedUpdate()
    {
        if (levelComp == true)
        {
            if (end == false)
            {
                blackFade.color = new Color(0, 0, 0, alpha);
            }
            else
            {
                blackFade.color = new Color(255, 255, 255, alpha);
            }
            alpha += .01f;
        }
        if (end == true || end2 == true)
        {
            if (oldTime + 2 <= Time.time)
            {
                song.volume -= .002f;
            }
            if (oldTime + 4 <= Time.time)
            {
                SceneManager.LoadScene(level);
            }
        }
        else
        {
            if (oldTime + 3 <= Time.time)
            {
                // Brings the player back to level select
                if (PlayerPrefs.GetInt("THISLEVELONLY") == 1)
                {
                    SceneManager.LoadScene(2);
                }
                else // Brings the player to the next level
                {
                    SceneManager.LoadScene(level);
                }
            }
        }
    }
    // Handle the achievements
    #region Acheiv
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
