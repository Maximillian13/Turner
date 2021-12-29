using UnityEngine;
using System.Collections;

public class ShowCursor : MonoBehaviour {

    private int counter;
    private Vector3 oldMousePos;

	// Use this for initialization
	void Start ()
    {
        counter = 0;
        oldMousePos = Input.mousePosition;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        counter++;

        if (counter == 300)
        {
            oldMousePos = Input.mousePosition;
        }


        // If the mouse input = the mouse input five seconds ago then hide the cursor. (Player is not using the mouse)
        if (oldMousePos == Input.mousePosition)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}
