// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SaveTheLevel : MonoBehaviour 
{
	// Saves the level that the player is on
    void Start()
    {
        // So there progress does not get reset if they start an lower level
        if (PlayerPrefs.GetInt("LEVEL") <= SceneManager.GetSceneAt(0).buildIndex)
        {
            PlayerPrefs.SetInt("LEVEL", SceneManager.GetSceneAt(0).buildIndex);
        }
    }
}
