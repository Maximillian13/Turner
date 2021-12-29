// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Steamworks;

public class PageButtonScripts : MonoBehaviour
{
    // Fields
    public static bool changed;
    public Text pageNameText;
    public Button previousPageButton;
    public Button nextPageButton;
    public Image pageImage;
    public Sprite lockedPage;
    public Sprite lockedPageSpanish;
    public Sprite[] pageSprite = new Sprite[20];

    // Translation
    public Text prevPageText;
    public Text nextPageText;
    public Text backText;
    private string lang;

    private int whichPage;
    private int[] pageUnlock = new int[20];
    private string[] pageName = new string[20];

	// Use this for initialization
	void Start () 
    {
        if(SteamManager.Initialized)
        {
            lang = SteamUtils.GetSteamUILanguage();
        }
        else
        {
            lang = "english";
        }

        // Translated
        if (lang.Equals("spanish"))
        {
            prevPageText.text = "Pagina Previa";
            nextPageText.text = "Pagina Siguiente";
            backText.text = "Regresar";
        }

        // Set Buttons
        whichPage = 0;
        previousPageButton.interactable = false;
        //previousPageButton.gameObject.SetActive(false);

        // Find whats unlocked
        pageUnlock[0] = PlayerPrefs.GetInt("PAGENUMBER0");
        pageUnlock[1] = PlayerPrefs.GetInt("PAGENUMBER1");
        pageUnlock[2] = PlayerPrefs.GetInt("PAGENUMBER2");
        pageUnlock[3] = PlayerPrefs.GetInt("PAGENUMBER3");
        pageUnlock[4] = PlayerPrefs.GetInt("PAGENUMBER4");
        pageUnlock[5] = PlayerPrefs.GetInt("PAGENUMBER5");
        pageUnlock[6] = PlayerPrefs.GetInt("PAGENUMBER6");
        pageUnlock[7] = PlayerPrefs.GetInt("PAGENUMBER7");
        pageUnlock[8] = PlayerPrefs.GetInt("PAGENUMBER8");
        pageUnlock[9] = PlayerPrefs.GetInt("PAGENUMBER9");
        pageUnlock[10] = PlayerPrefs.GetInt("PAGENUMBER10");
        pageUnlock[11] = PlayerPrefs.GetInt("PAGENUMBER11");
        pageUnlock[12] = PlayerPrefs.GetInt("PAGENUMBER12");
        pageUnlock[13] = PlayerPrefs.GetInt("PAGENUMBER13");
        pageUnlock[14] = PlayerPrefs.GetInt("PAGENUMBER14");
        pageUnlock[15] = PlayerPrefs.GetInt("PAGENUMBER15");
        pageUnlock[16] = PlayerPrefs.GetInt("PAGENUMBER16");
        pageUnlock[17] = PlayerPrefs.GetInt("PAGENUMBER17");
        pageUnlock[18] = PlayerPrefs.GetInt("PAGENUMBER18");
        pageUnlock[19] = PlayerPrefs.GetInt("PAGENUMBER19");
        //pageUnlock[20] = PlayerPrefs.GetInt("PAGENUMBER20");

        // Sets all names
        SetNames();
        pageNameText.text = pageName[0];
	}

    // Sets all names
    private void SetNames()
    {
        // Set Names
        if (lang.Equals("spanish"))
        {
			// Translated
			pageName[0] = "1. Olvidar";
			pageName[1] = "2. Pregunta";
			pageName[2] = "3. Intento";
			pageName[3] = "4. Visión I";
			pageName[4] = "5. Testarudos";
			pageName[5] = "6. Proximidad";
			pageName[6] = "7. Visión II";
			pageName[7] = "8. Desanimo";
			pageName[8] = "9. Visión III";
			pageName[9] = "10. Recuerda";
			pageName[10] = "11. Panico I";
			pageName[11] = "12. Observacion";
			pageName[12] = "13. Panico II";
			pageName[13] = "14. Paranoia";
			pageName[14] = "15. Perdido";
			pageName[15] = "16. Sin Esperanza";
			pageName[16] = "17. Conceder";
			pageName[17] = "18. Panico III";
			pageName[18] = "19. Odio";
			pageName[19] = "20. Repetir";
        }
        else
        {
			pageName[0] = "1. Forget";
			pageName[1] = "2. Question";
			pageName[2] = "3. Attempt";
			pageName[3] = "4. Vision I";
			pageName[4] = "5. Stubbornness";
			pageName[5] = "6. Proximity";
			pageName[6] = "7. Vision II";
			pageName[7] = "8. Discouragement";
			pageName[8] = "9. Vision III";
			pageName[9] = "10. Remember";
			pageName[10] = "11. Panic I";
			pageName[11] = "12. Observation";
			pageName[12] = "13. Panic II";
			pageName[13] = "14. Paranoia";
			pageName[14] = "15. Lost";
			pageName[15] = "16. Hopeless";
			pageName[16] = "17. Concede";
			pageName[17] = "18. Panic III";
			pageName[18] = "19. Hate";
			pageName[19] = "20. Repeat";
		}
    }
	
	// Update is called once per frame
	void Update () 
    {
        // Controls page unlock
        if (whichPage == 0 && pageUnlock[0] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[20];
            }
            else
            {
                pageImage.sprite = pageSprite[0];
            }
        }
        else if (whichPage == 1 && pageUnlock[1] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[21];
            }
            else
            {
                pageImage.sprite = pageSprite[1];
            }
        }
        else if (whichPage == 2 && pageUnlock[2] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[22];
            }
            else
            {
                pageImage.sprite = pageSprite[2];
            }
        }
        else if (whichPage == 3 && pageUnlock[3] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[23];
            }
            else
            {
                pageImage.sprite = pageSprite[3];
            }
        }
        else if (whichPage == 4 && pageUnlock[4] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[24];
            }
            else
            {
                pageImage.sprite = pageSprite[4];
            }
        }
        else if (whichPage == 5 && pageUnlock[5] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[25];
            }
            else
            {
                pageImage.sprite = pageSprite[5];
            }
        }
        else if (whichPage == 6 && pageUnlock[6] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[26];
            }
            else
            {
                pageImage.sprite = pageSprite[6];
            }
        }
        else if (whichPage == 7 && pageUnlock[7] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[27];
            }
            else
            {
                pageImage.sprite = pageSprite[7];
            }
        }
        else if (whichPage == 8 && pageUnlock[8] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[28];
            }
            else
            {
                pageImage.sprite = pageSprite[8];
            }
        }
        else if (whichPage == 9 && pageUnlock[9] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[29];
            }
            else
            {
                pageImage.sprite = pageSprite[9];
            }
        }
        else if (whichPage == 10 && pageUnlock[10] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[30];
            }
            else
            {
                pageImage.sprite = pageSprite[10];
            }
        }
        else if (whichPage == 11 && pageUnlock[11] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[31];
            }
            else
            {
                pageImage.sprite = pageSprite[11];
            }
        }
        else if (whichPage == 12 && pageUnlock[12] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[32];
            }
            else
            {
                pageImage.sprite = pageSprite[12];
            }
        }
        else if (whichPage == 13 && pageUnlock[13] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[33];
            }
            else
            {
                pageImage.sprite = pageSprite[13];
            }
        }
        else if (whichPage == 14 && pageUnlock[14] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[34];
            }
            else
            {
                pageImage.sprite = pageSprite[14];
            }
        }
        else if (whichPage == 15 && pageUnlock[15] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[35];
            }
            else
            {
                pageImage.sprite = pageSprite[15];
            }
        }
        else if (whichPage == 16 && pageUnlock[16] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[36];
            }
            else
            {
                pageImage.sprite = pageSprite[16];
            }
        }
        else if (whichPage == 17 && pageUnlock[17] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[37];
            }
            else
            {
                pageImage.sprite = pageSprite[17];
            }
        }
        else if (whichPage == 18 && pageUnlock[18] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[38];
            }
            else
            {
                pageImage.sprite = pageSprite[18];
            }
        }
        else if (whichPage == 19 && pageUnlock[19] == 1)
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = pageSprite[39];
            }
            else
            {
                pageImage.sprite = pageSprite[19];
            }
        }
        else
        {
            if (lang.Equals("spanish"))
            {
                pageImage.sprite = lockedPage;
                pageNameText.text = (whichPage + 1) + ". Locked Page";
            }
            else
            {
                // Translated
                pageImage.sprite = lockedPageSpanish;
                pageNameText.text = (whichPage + 1) + ". Pagina Cerrada";
            }
        }
        // Controls Name
	}

    // Changes pages and disables buttons accordingly 
    public void PreviousPage()
    {
        whichPage--;
        this.pageImage.transform.eulerAngles = new Vector3(0, 0, 0);
        if (whichPage == 0)
        {
            previousPageButton.interactable = false;
            //previousPageButton.gameObject.SetActive(false);
            nextPageButton.interactable = true;
            //nextPageButton.gameObject.SetActive(true);
        }
        else
        {
            previousPageButton.interactable = true;
            //previousPageButton.gameObject.SetActive(true);
            nextPageButton.interactable = true;
            //nextPageButton.gameObject.SetActive(true);
        }
        pageNameText.text = pageName[whichPage];
        changed = true;
    }

    // Changes pages and disables buttons accordingly 
    public void NextPage()
    {
        whichPage++;
        this.pageImage.transform.eulerAngles = new Vector3(0, 0, 0);
        if (whichPage == pageUnlock.Length - 1)
        {
            nextPageButton.interactable = false;
            //nextPageButton.gameObject.SetActive(false);
            previousPageButton.interactable = true;
            //previousPageButton.gameObject.SetActive(true);
        }
        else
        {
            nextPageButton.interactable = true;
            //nextPageButton.gameObject.SetActive(true);
            previousPageButton.interactable = true;
            //previousPageButton.gameObject.SetActive(true);
        }
        pageNameText.text = pageName[whichPage];
        changed = true;
    }
}
