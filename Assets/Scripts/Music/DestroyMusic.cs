using UnityEngine;
using System.Collections;

public class DestroyMusic : MonoBehaviour
{
    public string nameOfOldMusic;
    private GameObject oldMusic;
    // Use this for initialization
    void Awake()
    {
        oldMusic = GameObject.Find(nameOfOldMusic);
        if (oldMusic != null)
        {
            Destroy(oldMusic);
        }
    }
}
