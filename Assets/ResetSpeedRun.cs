using UnityEngine;
using System.Collections;

public class ResetSpeedRun : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        PlayerPrefs.SetInt("LEVELTIME", 0);
        PlayerPrefs.SetInt("TOTALTIME", 0);
        PlayerPrefs.SetInt("SPEEDRUN", 0);
        PlayerPrefs.SetInt("PREVLEV", 0);
	}

}
