using UnityEngine;
using System.Collections;

public class BeatGame : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        PlayerPrefs.SetInt("BEATGAME", 1);
        PlayerPrefs.SetInt("SPEEDRUNGOOD", 1);
	}
}
