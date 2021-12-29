using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResetLevelOnPlayerCommandSpecail : MonoBehaviour {

    public GameObject cube;
    public Rigidbody2D cubeRidge;
    public GameObject room;
    public GameObject turner;
    public Image deathFade;

    // Fields and Consts
    private float reset;
    private const float TimeToHold = .2f;

    // Set the timer back to 0
    void Start()
    {
        reset = 0;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

        // If the "R" key is being held start counting up, else set the time back to 0
        if (Input.GetButton("ResetLevel"))
        {
            reset += Time.deltaTime;
        }
        else
        {
            reset = 0;
        }

        // If the counter reaches the TimeToHold reset level
        if (reset > TimeToHold)
        {
            deathFade.CrossFadeAlpha(1, 0, false);
            deathFade.CrossFadeAlpha(0, .3f, false);
            cubeRidge.velocity = new Vector2(0, 0);
            cube.transform.position = new Vector2(-1, -2);
            room.transform.rotation = new Quaternion(0, 0, 0, 0);
            turner.transform.position = new Vector2(0.52f, -1.9f);
        }
    }
}
