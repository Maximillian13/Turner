using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour 
{
    private Text buttonText;
    private int originalFontSize;
    private int largFontSize;

    void Start()
    {
        Transform parent = this.transform.parent;
        GameObject buttonTextGO;
        buttonTextGO = parent.GetChild(0).gameObject;
        buttonText = buttonTextGO.GetComponent<Text>();
        originalFontSize = buttonText.fontSize;

        float big = originalFontSize * 1.5f;
        largFontSize = (int)big;
    }

    public void ButtonEnter()
    {
        buttonText.fontStyle = FontStyle.Bold;
        buttonText.fontSize = largFontSize;
    }

    public void ButtonExit()
    {
        buttonText.fontStyle = FontStyle.Normal;
        buttonText.fontSize = originalFontSize;
    }

    public void Select()
    {
        //buttonText.fontStyle = FontStyle.;
        buttonText.color = new Color(24, 100, 70);
    }
}
