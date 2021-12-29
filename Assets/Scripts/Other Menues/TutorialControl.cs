using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialControl : MonoBehaviour 
{
    public Image[] controlImage = new Image[2];
    private float oldTime;

    void Start()
    {
        oldTime = Time.time;
    }
	// Update is called once per frame
	void Update () 
    {
        if (controlImage[0] != null)
        {
            if (SceneManager.GetSceneAt(0).buildIndex == 5)
            {
                if (Input.GetAxis("Horizontal") == -1 || (Input.GetAxis("Horizontal") == 1))
                {
                    controlImage[0].CrossFadeAlpha(0, .75f, true);
                }
                if (Input.GetButton("RoomLeft") || Input.GetButton("RoomRight"))
                {
                    controlImage[1].CrossFadeAlpha(0, .75f, true);
                }
            }
            if (SceneManager.GetSceneAt(0).buildIndex == 9)
            {
                if (Input.GetButton("Jump"))
                {
                    controlImage[0].CrossFadeAlpha(0, .75f, true);
                }
            }
            if (SceneManager.GetSceneAt(0).buildIndex == 57)
            {
                if (Input.GetButton("Jump") && PlayerControls.grounded == false && (PlayerControls.againstWallLeft == true || PlayerControls.againstWallRight == true))
                {
                    controlImage[0].CrossFadeAlpha(0, .75f, true);
                }
            }
            else // Credits
            {
                if ((Input.GetButton("RoomLeft") || Input.GetButton("RoomRight")) || Time.time > oldTime + 4) 
                {
                    controlImage[0].CrossFadeAlpha(0, .75f, true);
                }
            }
        }

        // Other levels that require for controls
	}
}
