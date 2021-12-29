using UnityEngine;
using System.Collections;

public class Memory// : MonoBehaviour 
{

    private static bool axis;

    public static bool GetAxis()
    {
        return axis;
    }

    public static void SetAxis(bool moving)
    {
        axis = moving;
    }
}
