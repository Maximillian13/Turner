// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionSpecail : MonoBehaviour
{
    // Const 
    private const int NumberOfLevels = 100; // Game levels ( level 1, 2, 3 etc.)
    private const int NumberOfSceensBeforeLevels = 5; // This is talking about scenes like "Main menu" and "Pages"
    private const int LEVELS_OF_GAME = 443;

    // Public
    public Button previousLevelButton;
    public Button nextLevelButton;
    public Button btnStartFromHere;
    public Button btnJustThisLevel;
    public Button btnDarkWorld;
    public Text txtLevelName;
    public Image levelImageBottom;
    public Image levelImage;
    public Sprite[] levelSprite = new Sprite[NumberOfLevels];
    public Sprite[] botLevelSprite = new Sprite[6];
    public Sprite[] levelLockedSprites = new Sprite[10];

    // Private
    private bool[] levelUnlocked = new bool[NumberOfLevels];
    //private int[] pageInRoom = new int[NumberOfLevels];
    private string[] nameOfLevel = new string[NumberOfLevels];
    private int whichLevel;
    private int lastLevelPage;
    private int lastLockedPage;

    // Translation
    public Text chapterText;
    public Text quickSelectText;
    public Text prevLevelText;
    public Text nextLevelText;
    public Text worldSwapText;
    public Text playText;
    public Text continueText;
    public Text backText;
    private string lang;

    // Use this for initialization
    void Start()
    {
        if (SteamManager.Initialized)
        {
            lang = Steamworks.SteamUtils.GetSteamUILanguage();
        }
        else
        {
            lang = "english";
        }

        // Translated
        if (lang.Equals("spanish"))
        {
            // Translated
            chapterText.text = "Capitulo Cuatro: Comenzando";
            quickSelectText.text = "Seleccion Rapida";
            prevLevelText.text = "Nivel Rrevio";
            nextLevelText.text = "Nivel Siguiente";
            worldSwapText.text = "Mundo Distincto";
            playText.text = "Jugar";
            backText.text = "Regresar";
            continueText.text = "Continua";
        }

        previousLevelButton.interactable = false;
        if (PlayerPrefs.GetInt("BEATGAMESPECAIL") == 1)
        {
            btnDarkWorld.gameObject.SetActive(true);
        }
        else
        {
            btnDarkWorld.gameObject.SetActive(false);
        }
        whichLevel = 0;
        lastLevelPage = 0;
        lastLockedPage = 0;

        // Unlocks the levels that you have player
        if (PlayerPrefs.GetInt("LEVELSPECIAL") > NumberOfSceensBeforeLevels)
        {
            for (int x = 0; x < PlayerPrefs.GetInt("LEVELSPECIAL") + 1; x++)
            {
                levelUnlocked[x] = true;
            }
            levelUnlocked[0] = true;
            levelUnlocked[50] = true;
        }

        // Add a list of all the names
        NameAllLevels();

    }

    // Update is called once per frame
    void Update()
    {
        // Controls level unlock
        for (int x = 0; x < NumberOfLevels; x++)
        {
            LevelUnlock(x);
        }
        levelUnlocked[0] = true;
        levelUnlocked[50] = true;
    }

    private void LevelUnlock(int index)
    {
        // Check if the level is locked
        if (whichLevel == index && levelUnlocked[index] == true)
        {
            levelImage.sprite = levelSprite[index];
            txtLevelName.text = nameOfLevel[index];
            btnJustThisLevel.interactable = true;
            btnStartFromHere.interactable = true;
            return;
        }
        if (whichLevel == index && levelUnlocked[index] == false)
        {
            RandomLockedPage();
            if (index > 49)
            {
                if (lang == "spanish")
                {
                    txtLevelName.text = "-" + (index + 1 - 50) + ": Nivel Cerrada";
				}
				else
                {
                    txtLevelName.text = "-" + (index + 1 - 50) + ": Level Locked";
				}
			}
            else
            {
                if (lang == "spanish")
                {
					txtLevelName.text = (index + 1) + ": Nivel Cerrada";
				}
				else
                {
                    txtLevelName.text = (index + 1) + ": Level Locked";
                }
            }
            btnJustThisLevel.interactable = false;
            btnStartFromHere.interactable = false;
        }
    }

    // This will control the input field
    public void QuickLevelChange()
    {
        string value = "0";

        if (GameObject.Find("QuickSelectText") != null)
        {
            Text partValue = GameObject.Find("QuickSelectText").GetComponent<Text>();
            value = partValue.text;
        }
        
        int num;
        bool result = int.TryParse(value, out num);
        if (result == true)
        {
            int level = int.Parse(value);
            if (level > 0 && level <= 50)
            {
                if (whichLevel >= 50)
                {
                    whichLevel = level + 50 - 1;
                }
                else
                {
                    whichLevel = level - 1;
                }

                if (whichLevel == 0 || whichLevel == 50)
                {
                    previousLevelButton.interactable = false;
                }
                else
                {
                    previousLevelButton.interactable = true;
                }
                if (whichLevel == 49 || whichLevel == 99)
                {
                    nextLevelButton.interactable = false;
                }
                else
                {
                    nextLevelButton.interactable = true;
                }
            }
        }
    }

    // Starts the level from here then plays like normal
    public void StartFromHere()
    {
        GameObject music = GameObject.FindWithTag("Music");
        if (music != null)
        {
            Destroy(music);
        }
        if (whichLevel == 0)
        {
            SceneManager.LoadScene(544);
        }
        else if (whichLevel == 50)
        {
            SceneManager.LoadScene(548);
        }
        else
        {
            SceneManager.LoadScene(whichLevel + LEVELS_OF_GAME);
        }
    }

    public void Continue()
    {
        GameObject music = GameObject.FindWithTag("Music");
        if (music != null)
        {
            Destroy(music);
        }
        if (PlayerPrefs.GetInt("LEVELSPECIAL") == 0)
        {
            SceneManager.LoadScene(544);
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LEVELSPECIAL") + LEVELS_OF_GAME);
        }
    }

    // Changes Level and disables buttons accordingly 
    public void PreviousPage()
    {
        whichLevel--;
        if (whichLevel == 0 || whichLevel == 50)
        {
            previousLevelButton.interactable = false;
            nextLevelButton.interactable = true;
        }
        else
        {
            previousLevelButton.interactable = true;
            nextLevelButton.interactable = true;
        }
        RandomBackPage();
    }

    // Changes Level and disables buttons accordingly 
    public void NextPage()
    {
        whichLevel++;
        if (whichLevel == 49 || whichLevel == 99)
        {
            nextLevelButton.interactable = false;
            previousLevelButton.interactable = true;
        }
        else
        {
            nextLevelButton.interactable = true;
            previousLevelButton.interactable = true;
        }

        RandomBackPage();
    }

    public void DarkWorldToggle()
    {
        if (whichLevel <= 49)
        {
            whichLevel += 50;

            if (whichLevel == 99)
            {
                nextLevelButton.interactable = false;
                previousLevelButton.interactable = true;
            }
           
            if (whichLevel == 49)
            {
                previousLevelButton.interactable = false;
                nextLevelButton.interactable = true;
            }

            RandomBackPage();
        }
        else
        {
            whichLevel -= 50;
            if (whichLevel == 49)
            {
                nextLevelButton.interactable = false;
                previousLevelButton.interactable = true;
            }
            else
            {
                nextLevelButton.interactable = true;
                previousLevelButton.interactable = true;
            }
            if (whichLevel == 0)
            {
                previousLevelButton.interactable = false;
                nextLevelButton.interactable = true;
            }
            else
            {
                previousLevelButton.interactable = true;
                nextLevelButton.interactable = true;
            }
            RandomBackPage();
        }
    }

    // This will make a random background page show up behind the level
    private void RandomBackPage()
    {
        int randInt;
        do
        {
            randInt = Random.Range(0, botLevelSprite.Length);
        } while (randInt == lastLevelPage);

        levelImageBottom.sprite = botLevelSprite[randInt];
        lastLevelPage = randInt;
    }

    private void RandomLockedPage()
    {
        int randInt;
        do
        {
            randInt = Random.Range(0, levelLockedSprites.Length);
        } while (randInt == lastLockedPage);

        levelImage.sprite = levelLockedSprites[randInt];
        lastLockedPage = randInt;
    }

    // Assign all of the levels names 
    private void NameAllLevels()
    {
        if (lang.Equals("spanish"))
        {
			// Normal world
			nameOfLevel[0] = "1. Sala De Locos";
			nameOfLevel[1] = "2. Yarda";
			nameOfLevel[2] = "3. Combinacion";
			nameOfLevel[3] = "4. Tras Las Rejas";
			nameOfLevel[4] = "5. Liquido De Los Frenos";
			nameOfLevel[5] = "6. Compruebe La Barbilla";
			nameOfLevel[6] = "7. Ola De Calor";
			nameOfLevel[7] = "8. Todo El Dia";
			nameOfLevel[8] = "9. Todo El Dia Y Toda La Noche";
			nameOfLevel[9] = "10. Piense Dentro De La Celula";
			nameOfLevel[10] = "11. Piense Fuera De La Celula";
			nameOfLevel[11] = "12. Chaqueta";
			nameOfLevel[12] = "13. Cometa";
			nameOfLevel[13] = "14. No Fumar";
			nameOfLevel[14] = "15. Cowboys";
			nameOfLevel[15] = "16. prisión Gard";
			nameOfLevel[16] = "17. Prohibido";
			nameOfLevel[17] = "18. Coger Un Par";
			nameOfLevel[18] = "19. Bucky";
			nameOfLevel[19] = "20. Celly";
			nameOfLevel[20] = "21. La Delación";
			nameOfLevel[21] = "22. informante Seca";
			nameOfLevel[22] = "23. Cafeteria";
			nameOfLevel[23] = "24. Casa";
			nameOfLevel[24] = "25. J-Gato";
			nameOfLevel[25] = "26. Discapacitado";
			nameOfLevel[26] = "27. Sacudir Abajo";
			nameOfLevel[27] = "28. Sacudir Arriba";
			nameOfLevel[28] = "29. Eslabón De La Cadena";
			nameOfLevel[29] = "30. Alambre De Puas";
			nameOfLevel[30] = "31. Centro De Control";
			nameOfLevel[31] = "32. Centro De Control Parte Dos";
			nameOfLevel[32] = "33. Centro De Control Parte Tres";
			nameOfLevel[33] = "34. Centro De Control Parte Cuatro";
			nameOfLevel[34] = "35. El Patio";
			nameOfLevel[35] = "36. La Corte";
			nameOfLevel[36] = "37. La Cerca";
			nameOfLevel[37] = "38. La Cerca Parte Dos";
			nameOfLevel[38] = "39. Reloj Roto";
			nameOfLevel[39] = "40. Reloj Roto Parte Dos";
			nameOfLevel[40] = "41. Reloj Roto Parte Tres";
			nameOfLevel[41] = "42. Libertad Condicional";
			nameOfLevel[42] = "43. Puntos De Comprobación";
			nameOfLevel[43] = "44. Puntos De Comprobación Parte Dos";
			nameOfLevel[44] = "45. Confinamiento Solitario";
			nameOfLevel[45] = "46. Confinamiento Solitario Parte Dos";
			nameOfLevel[46] = "47. Confinamiento Solitario Parte Tres";
			nameOfLevel[47] = "48. Bloque De Celdas 071516";
			nameOfLevel[48] = "49. Bloque De Celdas 071516 Parte Dos";
			nameOfLevel[49] = "50. Celda 5150";

			// Dark world
			nameOfLevel[50] = "-1. Sala De Locos";
			nameOfLevel[51] = "-2. Yarda";
			nameOfLevel[52] = "-3. Combinacion";
			nameOfLevel[53] = "-4. Tras Las Rejas";
			nameOfLevel[54] = "-5. Liquido De Los Frenos";
			nameOfLevel[55] = "-6. Compruebe La Barbilla";
			nameOfLevel[56] = "-7. Ola De Calor";
			nameOfLevel[57] = "-8. Todo El Dia";
			nameOfLevel[58] = "-9. Todo El Dia Y Toda La Noche";
			nameOfLevel[59] = "-10. Piense Dentro De La Celula";
			nameOfLevel[60] = "-11. Piense Fuera De La Celula";
			nameOfLevel[61] = "-12. Chaqueta";
			nameOfLevel[62] = "-13. Cometa";
			nameOfLevel[63] = "-14. No Fumar";
			nameOfLevel[64] = "-15. Cowboys";
			nameOfLevel[65] = "-16. prisión Gard";
			nameOfLevel[66] = "-17. Prohibido";
			nameOfLevel[67] = "-18. Coger Un Par";
			nameOfLevel[68] = "-19. Bucky";
			nameOfLevel[69] = "-20. Celly";
			nameOfLevel[70] = "-21. La Delación";
			nameOfLevel[71] = "-22. informante Seca";
			nameOfLevel[72] = "-23. Cafeteria";
			nameOfLevel[73] = "-24. Casa";
			nameOfLevel[74] = "-25. J-Gato";
			nameOfLevel[75] = "-26. Discapacitado";
			nameOfLevel[76] = "-27. Sacudir Abajo";
			nameOfLevel[77] = "-28. Sacudir Arriba";
			nameOfLevel[78] = "-29. Eslabón De La Cadena";
			nameOfLevel[79] = "-30. Alambre De Puas";
			nameOfLevel[80] = "-31. Centro De Control";
			nameOfLevel[81] = "-32. Centro De Control Parte Dos";
			nameOfLevel[82] = "-33. Centro De Control Parte Tres";
			nameOfLevel[83] = "-34. Centro De Control Parte Cuatro";
			nameOfLevel[84] = "-35. El Patio";
			nameOfLevel[85] = "-36. La Corte";
			nameOfLevel[86] = "-37. La Cerca";
			nameOfLevel[87] = "-38. La Cerca Parte Dos";
			nameOfLevel[88] = "-39. Reloj Roto";
			nameOfLevel[89] = "-40. Reloj Roto Parte Dos";
			nameOfLevel[90] = "-41. Reloj Roto Parte Tres";
			nameOfLevel[91] = "-42. Libertad Condicional";
			nameOfLevel[92] = "-43. Puntos De Comprobación";
			nameOfLevel[93] = "-44. Puntos De Comprobación Parte Dos";
			nameOfLevel[94] = "-45. Confinamiento Solitario";
			nameOfLevel[95] = "-46. Confinamiento Solitario Parte Dos";
			nameOfLevel[96] = "-47. Confinamiento Solitario Parte Tres";
			nameOfLevel[97] = "-48. Bloque De Celdas 071516";
			nameOfLevel[98] = "-49. Bloque De Celdas 071516 Parte Dos";
			nameOfLevel[99] = "-50. Celda 5150";	
        }
        else
        {
			// Normal world
			nameOfLevel[0] = "1. Ding Wing";
			nameOfLevel[1] = "2. Rec Yard";
			nameOfLevel[2] = "3. Combo";
			nameOfLevel[3] = "4. Behind Bars";
			nameOfLevel[4] = "5. Brake Fluid";
			nameOfLevel[5] = "6. Chin Check";
			nameOfLevel[6] = "7. Heat Wave";
			nameOfLevel[7] = "8. All Day";
			nameOfLevel[8] = "9. All Day And All Night";
			nameOfLevel[9] = "10. Think Inside The Cell";
			nameOfLevel[10] = "11. Think Outside The Cell";
			nameOfLevel[11] = "12. Jacket";
			nameOfLevel[12] = "13. Kite";
			nameOfLevel[13] = "14. No Smoke";
			nameOfLevel[14] = "15. Cow Boys";
			nameOfLevel[15] = "16. Prison Gard";
			nameOfLevel[16] = "17. Barred";
			nameOfLevel[17] = "18. Catch A Pair";
			nameOfLevel[18] = "19. Bucky";
			nameOfLevel[19] = "20. Celly";
			nameOfLevel[20] = "21. Snitching";
			nameOfLevel[21] = "22. Dry Snitching";
			nameOfLevel[22] = "23. Cafeteria";
			nameOfLevel[23] = "24. House";
			nameOfLevel[24] = "25. J-Cat";
			nameOfLevel[25] = "26. Lame Duck";
			nameOfLevel[26] = "27. Shakedown";
			nameOfLevel[27] = "28. Shakeup";
			nameOfLevel[28] = "29. Chain Link";
			nameOfLevel[29] = "30. Razor Wire";
			nameOfLevel[30] = "31. Control Center";
			nameOfLevel[31] = "32. Control Center Part Two";
			nameOfLevel[32] = "33. Control Center Part Three";
			nameOfLevel[33] = "34. Control Center Part Four";
			nameOfLevel[34] = "35. The Yard";
			nameOfLevel[35] = "36. The Court";
			nameOfLevel[36] = "37. The Fence";
			nameOfLevel[37] = "38. The Fence Part Two";
			nameOfLevel[38] = "39. Broken Clock";
			nameOfLevel[39] = "40. Broken Clock Part Two";
			nameOfLevel[40] = "41. Broken Clock Part Three";
			nameOfLevel[41] = "42. Parole";
			nameOfLevel[42] = "43. Check Points";
			nameOfLevel[43] = "44. Check Points Part Two";
			nameOfLevel[44] = "45. Solitary Confinement";
			nameOfLevel[45] = "46. Solitary Confinement Part Two";
			nameOfLevel[46] = "47. Solitary Confinement Part Three";
			nameOfLevel[47] = "48. Cell Block 071516";
			nameOfLevel[48] = "49. Cell Block 071516 Part Two";
			nameOfLevel[49] = "50. Cell 5150";

			// Dark world
			nameOfLevel[50] = "-1. Ding Wing";
			nameOfLevel[51] = "-2. Rec Yard";
			nameOfLevel[52] = "-3. Combo";
			nameOfLevel[53] = "-4. Behind Bars";
			nameOfLevel[54] = "-5. Brake Fluid";
			nameOfLevel[55] = "-6. Chin Check";
			nameOfLevel[56] = "-7. Heat Wave";
			nameOfLevel[57] = "-8. All Day";
			nameOfLevel[58] = "-9. All Day And All Night";
			nameOfLevel[59] = "-10. Think Inside The Cell";
			nameOfLevel[60] = "-11. Think Outside The Cell";
			nameOfLevel[61] = "-12. Jacket";
			nameOfLevel[62] = "-13. Kite";
			nameOfLevel[63] = "-14. No Smoke";
			nameOfLevel[64] = "-15. Cow Boys";
			nameOfLevel[65] = "-16. Prison Gard";
			nameOfLevel[66] = "-17. Barred";
			nameOfLevel[67] = "-18. Catch A Pair";
			nameOfLevel[68] = "-19. Bucky";
			nameOfLevel[69] = "-20. Celly";
			nameOfLevel[70] = "-21. Snitching";
			nameOfLevel[71] = "-22. Dry Snitching";
			nameOfLevel[72] = "-23. Cafeteria";
			nameOfLevel[73] = "-24. House";
			nameOfLevel[74] = "-25. J-Cat";
			nameOfLevel[75] = "-26. Lame Duck";
			nameOfLevel[76] = "-27. Shakedown";
			nameOfLevel[77] = "-28. Shakeup";
			nameOfLevel[78] = "-29. Chain Link";
			nameOfLevel[79] = "-30. Razor Wire";
			nameOfLevel[80] = "-31. Control Center";
			nameOfLevel[81] = "-32. Control Center Part Two";
			nameOfLevel[82] = "-33. Control Center Part Three";
			nameOfLevel[83] = "-34. Control Center Part Four";
			nameOfLevel[84] = "-35. The Yard";
			nameOfLevel[85] = "-36. The Court";
			nameOfLevel[86] = "-37. The Fence";
			nameOfLevel[87] = "-38. The Fence Part Two";
			nameOfLevel[88] = "-39. Broken Clock";
			nameOfLevel[89] = "-40. Broken Clock Part Two";
			nameOfLevel[90] = "-41. Broken Clock Part Three";
			nameOfLevel[91] = "-42. Parole";
			nameOfLevel[92] = "-43. Check Points";
			nameOfLevel[93] = "-44. Check Points Part Two";
			nameOfLevel[94] = "-45. Solitary Confinement";
			nameOfLevel[95] = "-46. Solitary Confinement Part Two";
			nameOfLevel[96] = "-47. Solitary Confinement Part Three";
			nameOfLevel[97] = "-48. Cell Block 071516";
			nameOfLevel[98] = "-49. Cell Block 071516 Part Two";
			nameOfLevel[99] = "-50. Cell 5150";
		}
    }
}
