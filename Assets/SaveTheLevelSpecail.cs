// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SaveTheLevelSpecail : MonoBehaviour
{
    const int LEVELS_BEFORE = 443;
    // Saves the level that the player is on
    void Start()
    {
        // So there progress does not get reset if they start an lower level
        if (PlayerPrefs.GetInt("LEVELSPECIAL") <= SceneManager.GetSceneAt(0).buildIndex - LEVELS_BEFORE)
        {
            PlayerPrefs.SetInt("LEVELSPECIAL", SceneManager.GetSceneAt(0).buildIndex - LEVELS_BEFORE);
        }
    }
}
