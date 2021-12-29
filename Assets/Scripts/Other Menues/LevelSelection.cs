// Written by Maximillian Coburn, Property of Bean Boy Games, LLC.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    // Const 
    private const int NumberOfLevels = 410; // Game levels ( level 1, 2, 3 etc.)
    private const int NumberOfSceensBeforeLevels = 5; // This is talking about scenes like "Main menu" and "Pages"

    // Public
    public Button previousLevelButton;
    public Button nextLevelButton;
    public Button btnStartFromHere;
    public Button btnJustThisLevel;
    public Button btnDarkWorld;
    public Text txtLevelName;
    public Text txtChapterName;
    public Image levelImageBottom;
    public Image levelImage;
    public Image pageImage;
    public Sprite[] pageSprites = new Sprite[2];
    public Sprite[] levelSprite = new Sprite[NumberOfLevels];
    public Sprite[] botLevelSprite = new Sprite[6];
    public Sprite[] levelLockedSprites = new Sprite[10];

    // Private
    private bool[] levelUnlocked = new bool[NumberOfLevels];
    private int[] pageInRoom = new int[NumberOfLevels]; 
    private string[] nameOfLevel = new string[NumberOfLevels];
    private int whichLevel;
    private int lastLevelPage;
    private int lastLockedPage;


    // Translation
    public Text quickSelectText;
    public Text prevLevelText;
    public Text nextLevelText;
    public Text worldSwapText;
    public Text playText;
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
            quickSelectText.text = "Seleccion Rapida";
            prevLevelText.text = "Nivel Rrevio";
            nextLevelText.text = "Nivel Siguiente";
            worldSwapText.text = "Mundo Distincto";
            playText.text = "Jugar";
            backText.text = "Regresar";
        }

        // Set Buttons
        if (PlayerPrefs.GetInt("WHATLEVELONLEVELSELECT") == 0)
        {
            previousLevelButton.interactable = false;
        }
        if (PlayerPrefs.GetInt("BEATGAME") == 1)
        {
            btnDarkWorld.gameObject.SetActive(true);
        }
        else
        {
            btnDarkWorld.gameObject.SetActive(false);
        }
        whichLevel = PlayerPrefs.GetInt("WHATLEVELONLEVELSELECT");
        lastLevelPage = 0;
        lastLockedPage = 0;

        PlayerPrefs.SetInt("THISLEVELONLY", 0);

        // Unlocks the levels that you have player
        if (PlayerPrefs.GetInt("LEVEL") > NumberOfSceensBeforeLevels)
        {
            for (int x = 0; x < PlayerPrefs.GetInt("LEVEL") - NumberOfSceensBeforeLevels + 1; x++)
            {
                levelUnlocked[x] = true;
            }
        }

        // Add a list of all the names
        NameAllLevels();

        // Assign the levels that have pages
        PageAllLevels();

        // Do page bs
        DecideToShowPage(0);

        DecideWhatChapter();
    }

    // Update is called once per frame
    void Update()
    {
        // Controls level unlock
        for (int x = 0; x < NumberOfLevels; x++)
        {
            LevelUnlock(x);
        }

    }

    private void LevelUnlock(int index)
    {
        // Check if the level is locked
        if (whichLevel == index && levelUnlocked[index] == true)
        {
            levelImage.sprite = levelSprite[index];
            txtLevelName.text = nameOfLevel[index];        
            DecideToShowPage(index);
            btnJustThisLevel.interactable = true;
            btnStartFromHere.interactable = true;
            return;
        }
        if (whichLevel == index && levelUnlocked[index] == false)
        {
            RandomLockedPage();
            if (index > 204)
            {
                if (lang == "spanish")
                {
					txtLevelName.text = (index + 1 - 205) + ": Nivel Cerrada";
                }
                else
                {
					txtLevelName.text = (index + 1 - 205) + ": Level Locked";
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
    public void QuickLevelChange(string value)
    {
        int num;
        bool result = int.TryParse(value, out num);
        if (result == true)
        {
            int level = int.Parse(value);
            if (level > 0 && level <= 205)
            {
                if (whichLevel >= 205)
                {
                    whichLevel = level + 205 - 1;
                }
                else
                {
                    whichLevel = level - 1;
                }

                if (whichLevel == 0 || whichLevel == 205)
                {
                    previousLevelButton.interactable = false;
                }
                else
                {
                    previousLevelButton.interactable = true;
                }
                if (whichLevel == 204 || whichLevel == 409)
                {
                    nextLevelButton.interactable = false;
                }
                else
                {
                    nextLevelButton.interactable = true;
                }
                DecideWhatChapter();
            }
        }
    }

    // Starts the level from here then plays like normal
    public void StartFromHere()
    {
        GameObject music = GameObject.FindWithTag("Music");
        if(music != null)
        {
            Destroy(music);
        }
        SceneManager.LoadScene(whichLevel + NumberOfSceensBeforeLevels);
    }

    // Starts the level from here and when the level is complete returns back to level select
    public void ThisLevelOnly()
    {
        PlayerPrefs.SetInt("THISLEVELONLY", 1);
        PlayerPrefs.SetInt("WHATLEVELONLEVELSELECT", whichLevel);
        SceneManager.LoadScene(whichLevel + NumberOfSceensBeforeLevels);
    }

    // Changes Level and disables buttons accordingly 
    public void PreviousPage()
    {
        whichLevel--;
        if (whichLevel == 0 || whichLevel == 205)
        {
            previousLevelButton.interactable = false;
            nextLevelButton.interactable = true;
        }
        else
        {
            previousLevelButton.interactable = true;
            nextLevelButton.interactable = true;
        }

        DecideWhatChapter();
        RandomBackPage();
    }

    // Changes Level and disables buttons accordingly 
    public void NextPage()
    {
        whichLevel++;
        if (whichLevel == 204 || whichLevel == 409)
        {
            nextLevelButton.interactable = false;
            previousLevelButton.interactable = true;
        }
        else
        {
            nextLevelButton.interactable = true;
            previousLevelButton.interactable = true;
        }

        DecideWhatChapter();
        RandomBackPage();
    }

    public void DarkWorldToggle()
    {
        if(whichLevel <= 204)
        {
            whichLevel += 205;

            if (whichLevel == 409)
            {
                nextLevelButton.interactable = false;
                previousLevelButton.interactable = true;
            }
            //else
            //{
            //    nextLevelButton.interactable = true;
            //    previousLevelButton.interactable = true;
            //}
                    
            if (whichLevel == 205)
            {
                previousLevelButton.interactable = false;
                nextLevelButton.interactable = true;
            }
            //else
            //{
            //    previousLevelButton.interactable = true;
            //    nextLevelButton.interactable = true;
            //}

            DecideWhatChapter();
            RandomBackPage();
        }
        else
        {
            whichLevel -= 205;
            if (whichLevel == 205)
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

            DecideWhatChapter();
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

    // Assign all of the levels that have pages on them
    private void PageAllLevels()
    {
        // 0 = In room but not picked up, 1 = In room and picked up, 2 = Not in room.
        // Set them all to being not active to start
        for (int x = 0; x < NumberOfLevels; x++)
        {
            pageInRoom[x] = 2;
        }
        // Add the levels that have the pages in them
        pageInRoom[0] = PlayerPrefs.GetInt("PAGENUMBER0");
        pageInRoom[10] = PlayerPrefs.GetInt("PAGENUMBER1");
        pageInRoom[19] = PlayerPrefs.GetInt("PAGENUMBER2");
        pageInRoom[37] = PlayerPrefs.GetInt("PAGENUMBER3");
        pageInRoom[49] = PlayerPrefs.GetInt("PAGENUMBER4");
        pageInRoom[52] = PlayerPrefs.GetInt("PAGENUMBER5");
        pageInRoom[57] = PlayerPrefs.GetInt("PAGENUMBER6");
        pageInRoom[71] = PlayerPrefs.GetInt("PAGENUMBER7");
        pageInRoom[95] = PlayerPrefs.GetInt("PAGENUMBER8");
        pageInRoom[99] = PlayerPrefs.GetInt("PAGENUMBER9");
        pageInRoom[117] = PlayerPrefs.GetInt("PAGENUMBER10");
        pageInRoom[132] = PlayerPrefs.GetInt("PAGENUMBER11");
        pageInRoom[136] = PlayerPrefs.GetInt("PAGENUMBER12");
        pageInRoom[143] = PlayerPrefs.GetInt("PAGENUMBER13");
        pageInRoom[147] = PlayerPrefs.GetInt("PAGENUMBER14");
        pageInRoom[152] = PlayerPrefs.GetInt("PAGENUMBER15");
        pageInRoom[163] = PlayerPrefs.GetInt("PAGENUMBER16");
        pageInRoom[179] = PlayerPrefs.GetInt("PAGENUMBER17");
        pageInRoom[196] = PlayerPrefs.GetInt("PAGENUMBER18");
        pageInRoom[203] = PlayerPrefs.GetInt("PAGENUMBER19");
    }

    private void DecideWhatChapter()
    {
        // Translated
        if (whichLevel <= 49)
        {
            if (lang.Equals("spanish"))
            {
				txtChapterName.text = "Capitulo Uno: Descubrimiento";
			}
			else
            {
                txtChapterName.text = "Chapter One: Discovery";
			}
		}
        else if (whichLevel <= 99)
        {
            if (lang.Equals("spanish"))
            {
                txtChapterName.text = "Capitulo Dos: Ensayos";
			}
			else
            {
                txtChapterName.text = "Chapter Two: Trials";
			}
		}
        else if (whichLevel <= 149)
        {
            if (lang.Equals("spanish"))
            {
                txtChapterName.text = "Capitulo Tres: Haciendose";
			}
			else
            {
                txtChapterName.text = "Chapter Three: Becoming";
			}
		}
        else if (whichLevel <= 188)
        {
            if (lang.Equals("spanish"))
            {
                txtChapterName.text = "Capitulo Cuatro: Comodidad";
			}
			else
            {
                txtChapterName.text = "Chapter Four: Comfort";
			}
		}
        else if (whichLevel <= 199)
        {
            if (lang.Equals("spanish"))
            {
                txtChapterName.text = "Capitulo Cinco: Fracaso";
			}
			else
            {
                txtChapterName.text = "Chapter Five: Failure";
			}
		}
        else if (whichLevel <= 204)
        {
            if (lang.Equals("spanish"))
            {
                txtChapterName.text = "Capitulo Seis: Realizacion";
			}
			else
            {
                txtChapterName.text = "Chapter Six: Realization";
			}
		}
        else
        {
            if (lang.Equals("spanish"))
            {
                txtChapterName.text = "Capitulo Siete: Procesar De Nuevo";
			}
			else
            {
                txtChapterName.text = "Chapter Seven: Retry";
			}
		}
    }

    // Does the logic to decide what picture to show.
    private void DecideToShowPage(int index)
    {
        if(pageInRoom[index] == 2)
        {
            pageImage.enabled = false;
        }
        else if (pageInRoom[index] == 1)
        {
            pageImage.enabled = true;
            if (lang == "spanish")
            {
                pageImage.sprite = pageSprites[3];
			}
			else
            {
                pageImage.sprite = pageSprites[1];
			}
		}
        else
        {
            pageImage.enabled = true;
            if (lang == "spanish")
            {
                pageImage.sprite = pageSprites[2];
            }
            else
            {
                pageImage.sprite = pageSprites[0];
            }
        }
        //Debug.Log(pageInRoom[index]);
    }

    // Assign all of the levels names
    private void NameAllLevels()
    {
        if (lang.Equals("spanish"))
        {
			//Translated
			nameOfLevel[0] = "1. De Nuevo";
			nameOfLevel[1] = "2. Aprendisaje De Las Cuerdas";
			nameOfLevel[2] = "3. El Gueco";
			nameOfLevel[3] = "4. Peligro, No Tocar";
			nameOfLevel[4] = "5. Conseguir Atraves";
			nameOfLevel[5] = "6. La Puerta";
			nameOfLevel[6] = "7. La Puerta Parte Dos";
			nameOfLevel[7] = "8. Distancia";
			nameOfLevel[8] = "9. Distancia Parte Dos";
			nameOfLevel[9] = "10. Poniendolo Todo Junto";
			nameOfLevel[10] = "11. Separacion";
			nameOfLevel[11] = "12. El Objeto Inmovible";
			nameOfLevel[12] = "13. Fuera";
			nameOfLevel[13] = "14. Fuera Parte Dos";
			nameOfLevel[14] = "15. Golpe Ligero";
			nameOfLevel[15] = "16. Boton Pegajoso";
			nameOfLevel[16] = "17. Que Cae";
			nameOfLevel[17] = "18. Salto Y Brinco";
			nameOfLevel[18] = "19. Salto Largo";
			nameOfLevel[19] = "20. Contra La Pared";
			nameOfLevel[20] = "21. Si No Puedes Llegar A Eso...";
			nameOfLevel[21] = "22. Laberinto";
			nameOfLevel[22] = "23. Forzar A Entrar";
			nameOfLevel[23] = "24. Forzar A Salir";
			nameOfLevel[24] = "25. Favor De No Pergarle Al Vidrio";
			nameOfLevel[25] = "26. Nuevo Mundo Valiente";
			nameOfLevel[26] = "27. Doble Caida";
			nameOfLevel[27] = "28. Ahora Hazo Mientras Ellos Ven";
			nameOfLevel[28] = "29. Empujando Atraves";
			nameOfLevel[29] = "30. Nuevo Mundo Valiente Parte Dos";
			nameOfLevel[30] = "31. Descenso Cronometrado";
			nameOfLevel[31] = "32. Salto A Través De";
			nameOfLevel[32] = "33. Bloqueo";
			nameOfLevel[33] = "34. Todo Lo Ve";
			nameOfLevel[34] = "35. Todo Lo Sabe";
			nameOfLevel[35] = "36. Rueda";
			nameOfLevel[36] = "37. 40,000 Voltios";
			nameOfLevel[37] = "38. Tic-Tac";
			nameOfLevel[38] = "39. Tic-Tac Parte Dos";
			nameOfLevel[39] = "40. Tic-Tac Parte Tres";
			nameOfLevel[40] = "41. Modo De Hombre";
			nameOfLevel[41] = "42. Modo De Mono";
			nameOfLevel[42] = "43. Cabeza Baja";
			nameOfLevel[43] = "44. Piez Rapidos";
			nameOfLevel[44] = "45. Piez Rapicos, Cabeza Baja";
			nameOfLevel[45] = "46. Sincronizacion";
			nameOfLevel[46] = "47. Sincronizacion Parte Dos";
			nameOfLevel[47] = "48. Acceso Restrinido";
			nameOfLevel[48] = "49. Cuarto De Control";
			nameOfLevel[49] = "50. Tunel 51";
			// Chapter 2
			nameOfLevel[50] = "51. Movimientos Nuevos";
			nameOfLevel[51] = "52. Acrobacias";
			nameOfLevel[52] = "53. Fuera De Alcance";
			nameOfLevel[53] = "54. Altura";
			nameOfLevel[54] = "55. Papa Caliente";
			nameOfLevel[55] = "56. Plataforma De Multiple Usos";
			nameOfLevel[56] = "57. R.H.B.O";
			nameOfLevel[57] = "58. Amenaza Mobil";
			nameOfLevel[58] = "59. Mandalo Para Abago";
			nameOfLevel[59] = "60. Tiralo";
			nameOfLevel[60] = "61. Saltalo";
			nameOfLevel[61] = "62. Atraccion Fatal";
			nameOfLevel[62] = "63. Rodando";
			nameOfLevel[63] = "64. Resbalando Hacia Arriba";
			nameOfLevel[64] = "65. Entre La espada Y La Pared";
			nameOfLevel[65] = "66. Resbalando Por La Grietas";
			nameOfLevel[66] = "67. Voltea Al Final Del Pasillo";
			nameOfLevel[67] = "68. Arriba Y Arriba";
			nameOfLevel[68] = "69. Ojo De La Cerradura";
			nameOfLevel[69] = "70. Abriendo Caminos";
			nameOfLevel[70] = "71. Escalando";
			nameOfLevel[71] = "72. Bajando Y Subiendo";
			nameOfLevel[72] = "73. Salto De Fe";
			nameOfLevel[73] = "74. Salto De Fe Parte Dos";
			nameOfLevel[74] = "75. Salto De Fe Parte Tres";
			nameOfLevel[75] = "76. Ojo Des De Arriba";
			nameOfLevel[76] = "77. Cambiar Alrededor";
			nameOfLevel[77] = "78. Rodando Parte Dos";
			nameOfLevel[78] = "79. Resbalando Hacia Arriba Parte Dos";
			nameOfLevel[79] = "80. Espada De Un Filo";
			nameOfLevel[80] = "81. Espada Double Filo";
			nameOfLevel[81] = "82. La Pared Desaparece";
			nameOfLevel[82] = "83. La Pared Desaparece Parte Dos";
			nameOfLevel[83] = "84. La Pared Desaparece Parte Tres";
			nameOfLevel[84] = "85. La Pared Desaparece Parte Quatro";
			nameOfLevel[85] = "86. Volteando Con Tus Ojos Cerrados";
			nameOfLevel[86] = "87. Fe";
			nameOfLevel[87] = "88. Sabiduria";
			nameOfLevel[88] = "89. Usando Tus Allrededores";
			nameOfLevel[89] = "90. Siego";
			nameOfLevel[90] = "91. Siego Parte Dos";
			nameOfLevel[91] = "92. Siego Parte Tres";
			nameOfLevel[92] = "93. Siego Parte Quatro";
			nameOfLevel[93] = "94. De Regresso Otra Vez, Otra Vez";
			nameOfLevel[94] = "95. Perdido";
			nameOfLevel[95] = "96. Division";
			nameOfLevel[96] = "97. Elevador";
			nameOfLevel[97] = "98. Elevador Parte Dos";
			nameOfLevel[98] = "99. Elevador Parte Tres";
			nameOfLevel[99] = "100. Escalon 101";
			// Chapter 3
			nameOfLevel[100] = "101. Doble Brinco";
			nameOfLevel[101] = "102. La Zanja";
			nameOfLevel[102] = "103. Resvaladero";
			nameOfLevel[103] = "104. Distancia Lejana";
			nameOfLevel[104] = "105. La Zanja Parte Dos";
			nameOfLevel[105] = "106. Tratando De Salir";
			nameOfLevel[106] = "107. O.R.H.V";
			nameOfLevel[107] = "108. Prevenir";
			nameOfLevel[108] = "109. Trepando Para Arriba";
			nameOfLevel[109] = "110. Trepando Para Arriba Parte Dos";
			nameOfLevel[110] = "111. SMB";
			nameOfLevel[111] = "112. Escalones Para Arriba";
			nameOfLevel[112] = "113. Boca Abago";
			nameOfLevel[113] = "114. Boca Abago Parte Dos";
			nameOfLevel[114] = "115. Caida Al Brincar";
			nameOfLevel[115] = "116. Impulso";
			nameOfLevel[116] = "117. Impulso Parte Dos";
			nameOfLevel[117] = "118. Para Abajo y Para Arriba";
			nameOfLevel[118] = "119. Derecha y Arriba";
			nameOfLevel[119] = "120. Conduccion";
			nameOfLevel[120] = "121. Agarra Fuerte";
			nameOfLevel[121] = "122. Fuera De Sincronia";
			nameOfLevel[122] = "123. Fuera De Sincronia Parte Dos";
			nameOfLevel[123] = "124. Alrededor Del Mundo";
			nameOfLevel[124] = "125. Tiempo";
			nameOfLevel[125] = "126. Tiempo Parte Dos";
			nameOfLevel[126] = "127. Velocidad Necesitada";
			nameOfLevel[127] = "128. Aqui Se Ha Ido";
			nameOfLevel[128] = "129. Aqui Se Ha Ido Parte Dos";
			nameOfLevel[129] = "130. Aqui Se Ha Ido Parte Tres";
			nameOfLevel[130] = "131. Tiempo Al Aire";
			nameOfLevel[131] = "132. Montar La Llanta";
			nameOfLevel[132] = "133. Montar La Llanta Parte Dos";
			nameOfLevel[133] = "134. Parpadeo";
			nameOfLevel[134] = "135. Dos Cuadras";
			nameOfLevel[135] = "136. Tiempo Esperazado";
			nameOfLevel[136] = "137. Détente y Sigue";
			nameOfLevel[137] = "138. Celulas";
			nameOfLevel[138] = "139. Celulas Parte Dos";
			nameOfLevel[139] = "140. Un Solto a Vez";
			nameOfLevel[140] = "141. Dos Partes De Sincronizacion";
			nameOfLevel[141] = "142. Volar a Travez";
			nameOfLevel[142] = "143. Volar a Travez Parte Dos";
			nameOfLevel[143] = "144. Volar a Travez Parte Tres";
			nameOfLevel[144] = "145. Un Paso  A La Vez";
			nameOfLevel[145] = "146. Cabesazo";
			nameOfLevel[146] = "147. Cabesazo Parte Dos";
			nameOfLevel[147] = "148. Ocultendo De La Vez";
			nameOfLevel[148] = "149. Ocultendo De La Vez Parte Dos";
			nameOfLevel[149] = "150. Corredor 151";
			// Chapter 4
			nameOfLevel[150] = "151. Pegajoso";
			nameOfLevel[151] = "152. Si No Puedes Posar";
			nameOfLevel[152] = "153. Dispara A J";
			nameOfLevel[153] = "154. Caido Lateral";
			nameOfLevel[154] = "155. Arriba Y Alrededor";
			nameOfLevel[155] = "156. Celulas Parte Tres";
			nameOfLevel[156] = "157. Aferran Salto";
			nameOfLevel[157] = "158. Agarrar Fuerte";
			nameOfLevel[158] = "159. Pastel";
			nameOfLevel[159] = "160. Abajo Y Arriba";
			nameOfLevel[160] = "161. Abajo Y Arriba Parte Dos";
			nameOfLevel[161] = "162. Abajo Y Arriba Parte Tres";
			nameOfLevel[162] = "163. Atravez De";
			nameOfLevel[163] = "164. En La Forma";
			nameOfLevel[164] = "165. Arriba Y Por Encima";
			nameOfLevel[165] = "166. Arriba Y Por Encima Parte Dos";
			nameOfLevel[166] = "167. Ve Mas Despacio";
			nameOfLevel[167] = "168. Pastel Parte Dos";
			nameOfLevel[168] = "169. De Un Carril";
			nameOfLevel[169] = "170. De Un Carril Parte Dos";
			nameOfLevel[170] = "171. Centro Del Blanco";
			nameOfLevel[171] = "172. Romper";
			nameOfLevel[172] = "173. Buen Ojo";
			nameOfLevel[173] = "174. Buen Ojo Parte Dos";
			nameOfLevel[174] = "175. Salto De Fe Parte Tres";
			nameOfLevel[175] = "176. Del Otro Lado";
			nameOfLevel[176] = "177. Aferra A Lo Que No Puedes Ver";
			nameOfLevel[177] = "178. Cegado";
			nameOfLevel[178] = "179. Reacciones Rapidas";
			nameOfLevel[179] = "180. Sonando De SK";
			nameOfLevel[180] = "181. Tu No Eres El Unico";
			nameOfLevel[181] = "182. Tu No Eres El Unico Parte Dos";
			nameOfLevel[182] = "183. Interferencia";
			nameOfLevel[183] = "184. Mi Alto";
			nameOfLevel[184] = "185. Mi Alto Parte Dos";
			nameOfLevel[185] = "186. Escoje Un Lado";
			nameOfLevel[186] = "187. PEscoje Un Lado Parte Dos";
			nameOfLevel[187] = "188. Catapulta";
			nameOfLevel[188] = "189. ?Libertad?";
			// Chapter 5
			nameOfLevel[189] = "!(). Qb";
			nameOfLevel[190] = "!(!. lbh";
			nameOfLevel[191] = "!(@. erzrzore";
			nameOfLevel[192] = "!(#. gung";
			nameOfLevel[193] = "!($. lbh";
			nameOfLevel[194] = "!(%. xvyyrq";
			nameOfLevel[195] = "!({. uvz?";
			nameOfLevel[196] = "!(}. Bs";
			nameOfLevel[197] = "!(*. pbhefr";
			nameOfLevel[198] = "!((. lbh";
			nameOfLevel[199] = "@)). qba'g...";
			// Chapter 6
			nameOfLevel[200] = "201. ?Los";
			nameOfLevel[201] = "202. Ultimos";
			nameOfLevel[202] = "203. Poco";
			nameOfLevel[203] = "204. Pasos";
			nameOfLevel[204] = "205. Escapar?";


			// Dark World
			nameOfLevel[205] = "-1. De Nuevo";
			nameOfLevel[206] = "-2. Aprendisaje De Las Cuerdas";
			nameOfLevel[207] = "-3. El Gueco";
			nameOfLevel[208] = "-4. Peligro, No Tocar";
			nameOfLevel[209] = "-5. Conseguir Atraves";
			nameOfLevel[210] = "-6. La Puerta";
			nameOfLevel[211] = "-7. La Puerta Parte Dos";
			nameOfLevel[212] = "-8. Distancia";
			nameOfLevel[213] = "-9. Distancia Parte Dos";
			nameOfLevel[214] = "-10. Poniendolo Todo Junto";
			nameOfLevel[215] = "-11. Separacion";
			nameOfLevel[216] = "-12. El Objeto Inmovible";
			nameOfLevel[217] = "-13. Fuera";
			nameOfLevel[218] = "-14. Fuera Parte Dos";
			nameOfLevel[219] = "-15. Golpe Ligero";
			nameOfLevel[220] = "-16. Boton Pegajoso";
			nameOfLevel[221] = "-17. Que Cae";
			nameOfLevel[222] = "-18. Salto Y Brinco";
			nameOfLevel[223] = "-19. Salto Largo";
			nameOfLevel[224] = "-20. Contra La Pared";
			nameOfLevel[225] = "-21. Si No Puedes Llegar A Eso...";
			nameOfLevel[226] = "-22. Laberinto";
			nameOfLevel[227] = "-23. Forzar A Entrar";
			nameOfLevel[228] = "-24. Forzar A Salir";
			nameOfLevel[229] = "-25. Favor De No Pergarle Al Vidrio";
			nameOfLevel[230] = "-26. Nuevo Mundo Valiente";
			nameOfLevel[231] = "-27. Doble Caida";
			nameOfLevel[232] = "-28. Ahora Hazo Mientras Ellos Ven";
			nameOfLevel[233] = "-29. Empujando Atraves";
			nameOfLevel[234] = "-30. Nuevo Mundo Valiente Parte Dos";
			nameOfLevel[235] = "-31. Descenso Cronometrado";
			nameOfLevel[236] = "-32. Salto A Través De";
			nameOfLevel[237] = "-33. Bloqueo";
			nameOfLevel[238] = "-34. Todo Lo Ve";
			nameOfLevel[239] = "-35. Todo Lo Sabe";
			nameOfLevel[240] = "-36. Rueda";
			nameOfLevel[241] = "-37. 40,000 Voltios";
			nameOfLevel[242] = "-38. Tic-Tac";
			nameOfLevel[243] = "-39. Tic-Tac Parte Dos";
			nameOfLevel[244] = "-40. Tic-Tac Parte Tres";
			nameOfLevel[245] = "-41. Modo De Hombre";
			nameOfLevel[246] = "-42. Modo De Mono";
			nameOfLevel[247] = "-43. Cabeza Baja";
			nameOfLevel[248] = "-44. Piez Rapidos";
			nameOfLevel[249] = "-45. Piez Rapicos, Cabeza Baja";
			nameOfLevel[250] = "-46. Sincronizacion";
			nameOfLevel[251] = "-47. Sincronizacion Parte Dos";
			nameOfLevel[252] = "-48. Acceso Restrinido";
			nameOfLevel[253] = "-49. Cuarto De Control";
			nameOfLevel[254] = "-50. Tunel 51";
			// Chapter 2
			nameOfLevel[255] = "-51. Movimientos Nuevos";
			nameOfLevel[256] = "-52. Acrobacias";
			nameOfLevel[257] = "-53. Fuera De Alcance";
			nameOfLevel[258] = "-54. Altura";
			nameOfLevel[259] = "-55. Papa Caliente";
			nameOfLevel[260] = "-56. Plataforma De Multiple Usos";
			nameOfLevel[261] = "-57. R.H.B.O";
			nameOfLevel[262] = "-58. Amenaza Mobil";
			nameOfLevel[263] = "-59. Mandalo Para Abago";
			nameOfLevel[264] = "-60. Tiralo";
			nameOfLevel[265] = "-61. Saltalo";
			nameOfLevel[266] = "-62. Atraccion Fatal";
			nameOfLevel[267] = "-63. Rodando";
			nameOfLevel[268] = "-64. Resbalando Hacia Arriba";
			nameOfLevel[269] = "-65. Entre La espada Y La Pared";
			nameOfLevel[270] = "-66. Resbalando Por La Grietas";
			nameOfLevel[271] = "-67. Voltea Al Final Del Pasillo";
			nameOfLevel[272] = "-68. Arriba Y Arriba";
			nameOfLevel[273] = "-69. Ojo De La Cerradura";
			nameOfLevel[274] = "-70. Abriendo Caminos";
			nameOfLevel[275] = "-71. Escalando";
			nameOfLevel[276] = "-72. Bajando Y Subiendo";
			nameOfLevel[277] = "-73. Salto De Fe";
			nameOfLevel[278] = "-74. Salto De Fe Parte Dos";
			nameOfLevel[279] = "-75. Salto De Fe Parte Tres";
			nameOfLevel[280] = "-76. Ojo Des De Arriba";
			nameOfLevel[281] = "-77. Cambiar Alrededor";
			nameOfLevel[282] = "-78. Rodando Parte Dos";
			nameOfLevel[283] = "-79. Resbalando Hacia Arriba Parte Dos";
			nameOfLevel[284] = "-80. Espada De Un Filo";
			nameOfLevel[285] = "-81. Espada Double Filo";
			nameOfLevel[286] = "-82. La Pared Desaparece";
			nameOfLevel[287] = "-83. La Pared Desaparece Parte Dos";
			nameOfLevel[288] = "-84. La Pared Desaparece Parte Tres";
			nameOfLevel[289] = "-85. La Pared Desaparece Parte Quatro";
			nameOfLevel[290] = "-86. Volteando Con Tus Ojos Cerrados";
			nameOfLevel[291] = "-87. Fe";
			nameOfLevel[292] = "-88. Sabiduria";
			nameOfLevel[293] = "-89. Usando Tus Allrededores";
			nameOfLevel[294] = "-90. Siego";
			nameOfLevel[295] = "-91. Siego Parte Dos";
			nameOfLevel[296] = "-92. Siego Parte Tres";
			nameOfLevel[297] = "-93. Siego Parte Quatro";
			nameOfLevel[298] = "-94. De Regresso Otra Vez, Otra Vez";
			nameOfLevel[299] = "-95. Perdido";
			nameOfLevel[300] = "-96. Division";
			nameOfLevel[301] = "-97. Elevador";
			nameOfLevel[302] = "-98. Elevador Parte Dos";
			nameOfLevel[303] = "-99. Elevador Parte Tres";
			nameOfLevel[304] = "-100. Escalon 101";
			// Chapter 3
			nameOfLevel[305] = "-101. Doble Brinco";
			nameOfLevel[306] = "-102. La Zanja";
			nameOfLevel[307] = "-103. Resvaladero";
			nameOfLevel[308] = "-104. Distancia Lejana";
			nameOfLevel[309] = "-105. La Zanja Parte Dos";
			nameOfLevel[310] = "-106. Tratando De Salir";
			nameOfLevel[311] = "-107. O.R.H.V";
			nameOfLevel[312] = "-108. Prevenir";
			nameOfLevel[313] = "-109. Trepando Para Arriba";
			nameOfLevel[314] = "-110. Trepando Para Arriba Parte Dos";
			nameOfLevel[315] = "-111. SMB";
			nameOfLevel[316] = "-112. Escalones Para Arriba";
			nameOfLevel[317] = "-113. Boca Abago";
			nameOfLevel[318] = "-114. Boca Abago Parte Dos";
			nameOfLevel[319] = "-115. Caida Al Brincar";
			nameOfLevel[320] = "-116. Impulso";
			nameOfLevel[321] = "-117. Impulso Parte Dos";
			nameOfLevel[322] = "-118. Para Abajo y Para Arriba";
			nameOfLevel[323] = "-119. Derecha y Arriba";
			nameOfLevel[324] = "-120. Conduccion";
			nameOfLevel[325] = "-121. Agarra Fuerte";
			nameOfLevel[326] = "-122. Fuera De Sincronia";
			nameOfLevel[327] = "-123. Fuera De Sincronia Parte Dos";
			nameOfLevel[328] = "-124. Alrededor Del Mundo";
			nameOfLevel[329] = "-125. Tiempo";
			nameOfLevel[330] = "-126. Tiempo Parte Dos";
			nameOfLevel[331] = "-127. Velocidad Necesitada";
			nameOfLevel[332] = "-128. Aqui Se Ha Ido";
			nameOfLevel[333] = "-129. Aqui Se Ha Ido Parte Dos";
			nameOfLevel[334] = "-130. Aqui Se Ha Ido Parte Tres";
			nameOfLevel[335] = "-131. Tiempo Al Aire";
			nameOfLevel[336] = "-132. Montar La Llanta";
			nameOfLevel[337] = "-133. Montar La Llanta Parte Dos";
			nameOfLevel[338] = "-134. Parpadeo";
			nameOfLevel[339] = "-135. Dos Cuadras";
			nameOfLevel[340] = "-136. Tiempo Esperazado";
			nameOfLevel[341] = "-137. Détente y Sigue";
			nameOfLevel[342] = "-138. Celulas";
			nameOfLevel[343] = "-139. Celulas Parte Dos";
			nameOfLevel[344] = "-140. Un Solto a Vez";
			nameOfLevel[345] = "-141. Dos Partes De Sincronizacion";
			nameOfLevel[346] = "-142. Volar a Travez";
			nameOfLevel[347] = "-143. Volar a Travez Parte Dos";
			nameOfLevel[348] = "-144. Volar a Travez Parte Tres";
			nameOfLevel[349] = "-145. Un Paso  A La Vez";
			nameOfLevel[350] = "-146. Cabesazo";
			nameOfLevel[351] = "-147. Cabesazo Parte Dos";
			nameOfLevel[352] = "-148. Ocultendo De La Vez";
			nameOfLevel[353] = "-149. Ocultendo De La Vez Parte Dos";
			nameOfLevel[354] = "-150. Corredor 151";
			// Chapter 4
			nameOfLevel[355] = "-151. Pegajoso";
			nameOfLevel[356] = "-152. Si No Puedes Posar";
			nameOfLevel[357] = "-153. Dispara A J";
			nameOfLevel[358] = "-154. Caido Lateral";
			nameOfLevel[359] = "-155. Arriba Y Alrededor";
			nameOfLevel[360] = "-156. Celulas Parte Tres";
			nameOfLevel[361] = "-157. Aferran Salto";
			nameOfLevel[362] = "-158. Agarrar Fuerte";
			nameOfLevel[363] = "-159. Pastel";
			nameOfLevel[364] = "-160. Abajo Y Arriba";
			nameOfLevel[365] = "-161. Abajo Y Arriba Parte Dos";
			nameOfLevel[366] = "-162. Abajo Y Arriba Parte Tres";
			nameOfLevel[367] = "-163. Atravez De";
			nameOfLevel[368] = "-164. En La Forma";
			nameOfLevel[369] = "-165. Arriba Y Por Encima";
			nameOfLevel[370] = "-166. Arriba Y Por Encima Parte Dos";
			nameOfLevel[371] = "-167. Ve Mas Despacio";
			nameOfLevel[372] = "-168. Pastel Parte Dos";
			nameOfLevel[373] = "-169. De Un Carril";
			nameOfLevel[374] = "-170. De Un Carril Parte Dos";
			nameOfLevel[375] = "-171. Centro Del Blanco";
			nameOfLevel[376] = "-172. Romper";
			nameOfLevel[377] = "-173. Buen Ojo";
			nameOfLevel[378] = "-174. Buen Ojo Parte Dos";
			nameOfLevel[379] = "-175. Salto De Fe Parte Tres";
			nameOfLevel[380] = "-176. Del Otro Lado";
			nameOfLevel[381] = "-177. Aferra A Lo Que No Puedes Ver";
			nameOfLevel[382] = "-178. Cegado";
			nameOfLevel[383] = "-179. Reacciones Rapidas";
			nameOfLevel[384] = "-180. Sonando De SK";
			nameOfLevel[385] = "-181. Tu No Eres El Unico";
			nameOfLevel[386] = "-182. Tu No Eres El Unico Parte Dos";
			nameOfLevel[387] = "-183. Interferencia";
			nameOfLevel[388] = "-184. Mi Alto";
			nameOfLevel[389] = "-185. Mi Alto Parte Dos";
			nameOfLevel[390] = "-186. Escoje Un Lado";
			nameOfLevel[391] = "-187. PEscoje Un Lado Parte Dos";
			nameOfLevel[392] = "-188. Catapulta";
			nameOfLevel[393] = "-189. ?Libertad?";
			// Chapter 5
			nameOfLevel[394] = "-!(). Qb";
			nameOfLevel[395] = "-!(!. lbh";
			nameOfLevel[396] = "-!(@. erzrzore";
			nameOfLevel[397] = "-!(#. gung";
			nameOfLevel[398] = "-!($. lbh";
			nameOfLevel[399] = "-!(%. xvyyrq";
			nameOfLevel[400] = "-!({. uvz?";
			nameOfLevel[401] = "-!(}. Bs";
			nameOfLevel[402] = "-!(*. pbhefr";
			nameOfLevel[403] = "-!((. lbh";
			nameOfLevel[404] = "-@)). qba'g...";
			// Chapter 6
			nameOfLevel[405] = "-201. ?Los";
			nameOfLevel[406] = "-202. Ultimos";
			nameOfLevel[407] = "-203. Poco";
			nameOfLevel[408] = "-204. Pasos";
			nameOfLevel[409] = "-205. Escapar?";
		}
        else
        {
			nameOfLevel[0] = "1. Back Again";
			nameOfLevel[1] = "2. Learning The Ropes";
			nameOfLevel[2] = "3. The Gap";
			nameOfLevel[3] = "4. Danger, Don't Touch!";
			nameOfLevel[4] = "5. Getting Through";
			nameOfLevel[5] = "6. The Gate";
			nameOfLevel[6] = "7. The Gate Part Two";
			nameOfLevel[7] = "8. Distance";
			nameOfLevel[8] = "9. Distance Part Two";
			nameOfLevel[9] = "10. Putting It All Together";
			nameOfLevel[10] = "11. Separation";
			nameOfLevel[11] = "12. The Immovable Object";
			nameOfLevel[12] = "13. Outside";
			nameOfLevel[13] = "14. Outside Part Two";
			nameOfLevel[14] = "15. Light Tap";
			nameOfLevel[15] = "16. Sticky Button";
			nameOfLevel[16] = "17. Falling Through";
			nameOfLevel[17] = "18. Hop, Skip, and a Jump";
			nameOfLevel[18] = "19. Long Jump";
			nameOfLevel[19] = "20. Against The Wall";
			nameOfLevel[20] = "21. If You Can't Get To It...";
			nameOfLevel[21] = "22. Maze";
			nameOfLevel[22] = "23. Break In";
			nameOfLevel[23] = "24. Break Out";
			nameOfLevel[24] = "25. Please Don't Tap Glass";
			nameOfLevel[25] = "26. Brave New World";
			nameOfLevel[26] = "27. Double Drop";
			nameOfLevel[27] = "28. Now Do It While They're Watching";
			nameOfLevel[28] = "29. Pushing Through";
			nameOfLevel[29] = "30. Brave New World Part Two";
			nameOfLevel[30] = "31. Timed Drop";
			nameOfLevel[31] = "32. Jumping Through";
			nameOfLevel[32] = "33. Blockage";
			nameOfLevel[33] = "34. All Seeing";
			nameOfLevel[34] = "35. All knowing";
			nameOfLevel[35] = "36. Wheel";
			nameOfLevel[36] = "37. 40,000 Volts";
			nameOfLevel[37] = "38. Tic-Tock";
			nameOfLevel[38] = "39. Tic-Tock Part Two";
			nameOfLevel[39] = "40. Tic-Tock Part Three";
			nameOfLevel[40] = "41. Man Mode";
			nameOfLevel[41] = "42. Monkey Mode";
			nameOfLevel[42] = "43. Head Low";
			nameOfLevel[43] = "44. Fast Feet";
			nameOfLevel[44] = "45. Fast Feet, Head Low";
			nameOfLevel[45] = "46. Timing";
			nameOfLevel[46] = "47. Timing Part Two";
			nameOfLevel[47] = "48. Restricted Access";
			nameOfLevel[48] = "49. Control Room";
			nameOfLevel[49] = "50. Tunnel 51";
			// Chapter 2
			nameOfLevel[50] = "51. New Moves";
			nameOfLevel[51] = "52. Acrobatics";
			nameOfLevel[52] = "53. Out of Reach";
			nameOfLevel[53] = "54. Height";
			nameOfLevel[54] = "55. Hot Potato";
			nameOfLevel[55] = "56. Multi-Purpose Platform";
			nameOfLevel[56] = "57. O.H.L.O";
			nameOfLevel[57] = "58. Mobile Threat";
			nameOfLevel[58] = "59. Send It Down";
			nameOfLevel[59] = "60. Drop It";
			nameOfLevel[60] = "61. Hop It";
			nameOfLevel[61] = "62. Fatal Attraction";
			nameOfLevel[62] = "63. Rolling";
			nameOfLevel[63] = "64. Sliding Up";
			nameOfLevel[64] = "65. Rock And A Hard Place";
			nameOfLevel[65] = "66. Slipping Through The Cracks";
			nameOfLevel[66] = "67. Turn At The End Of The Hall";
			nameOfLevel[67] = "68. Up And Up";
			nameOfLevel[68] = "69. Key Hole";
			nameOfLevel[69] = "70. Opening Paths";
			nameOfLevel[70] = "71. Climbing Up";
			nameOfLevel[71] = "72. Going Down, Going Up";
			nameOfLevel[72] = "73. Leap Of Faith";
			nameOfLevel[73] = "74. Leap Of Faith Part Two";
			nameOfLevel[74] = "75. Leap Of Faith Part Three";
			nameOfLevel[75] = "76. Eye From Above";
			nameOfLevel[76] = "77. Switched Around";
			nameOfLevel[77] = "78. Rolling Part Two";
			nameOfLevel[78] = "79. Sliding Up Part Two";
			nameOfLevel[79] = "80. Single Edged Sword";
			nameOfLevel[80] = "81. Double Edged Sword";
			nameOfLevel[81] = "82. The Disappearing Wall";
			nameOfLevel[82] = "83. The Disappearing Wall Part Two";
			nameOfLevel[83] = "84. The Disappearing Wall Part Three";
			nameOfLevel[84] = "85. The Disappearing Wall Part Four";
			nameOfLevel[85] = "86. Turning With Your Eyes Closed";
			nameOfLevel[86] = "87. Faith";
			nameOfLevel[87] = "88. Knowledge";
			nameOfLevel[88] = "89. Using Your Surroundings";
			nameOfLevel[89] = "90. Blind";
			nameOfLevel[90] = "91. Blind Part Two";
			nameOfLevel[91] = "92. Blind Part Three";
			nameOfLevel[92] = "93. Blind Part Four";
			nameOfLevel[93] = "94. Back Again, Again";
			nameOfLevel[94] = "95. Lost";
			nameOfLevel[95] = "96. Division";
			nameOfLevel[96] = "97. Elevator";
			nameOfLevel[97] = "98. Elevator Part Two";
			nameOfLevel[98] = "99. Elevator Part Three";
			nameOfLevel[99] = "100. Stairway 101";
			// Chapter 3
			nameOfLevel[100] = "101. Double Jump";
			nameOfLevel[101] = "102. The Ditch";
			nameOfLevel[102] = "103. Slide";
			nameOfLevel[103] = "104. Further Distance";
			nameOfLevel[104] = "105. The Ditch Part Two";
			nameOfLevel[105] = "106. Getting Out";
			nameOfLevel[106] = "107. G.O.H.L.O";
			nameOfLevel[107] = "108. Avoid";
			nameOfLevel[108] = "109. Crawling Up";
			nameOfLevel[109] = "110. Crawling Up Part Two";
			nameOfLevel[110] = "111. SMB";
			nameOfLevel[111] = "112. Steps Up";
			nameOfLevel[112] = "113. Upside-Down";
			nameOfLevel[113] = "114. Upside-Down Part Two";
			nameOfLevel[114] = "115. Falling Jump";
			nameOfLevel[115] = "116. Momentum";
			nameOfLevel[116] = "117. Momentum Part Two";
			nameOfLevel[117] = "118. Down And Up";
			nameOfLevel[118] = "119. Right And Up";
			nameOfLevel[119] = "120. Conduction";
			nameOfLevel[120] = "121. Hold Tight";
			nameOfLevel[121] = "122. Out Of Sync";
			nameOfLevel[122] = "123. Out Of Sync Part Two";
			nameOfLevel[123] = "124. Around The World";
			nameOfLevel[124] = "125. Time";
			nameOfLevel[125] = "126. Time Part Two";
			nameOfLevel[126] = "127. Needed Speed";
			nameOfLevel[127] = "128. Here And Gone";
			nameOfLevel[128] = "129. Here And Gone Part Two";
			nameOfLevel[129] = "130. Here And Gone Part Three";
			nameOfLevel[130] = "131. Air Time";
			nameOfLevel[131] = "132. Riding The Rim";
			nameOfLevel[132] = "133. Riding The Rim Part Two";
			nameOfLevel[133] = "134. Blink";
			nameOfLevel[134] = "135. Two Block";
			nameOfLevel[135] = "136. Hopeful Timing";
			nameOfLevel[136] = "137. Stop and Go";
			nameOfLevel[137] = "138. Cells";
			nameOfLevel[138] = "139. Cells Part Two";
			nameOfLevel[139] = "140. One Hop At A Time";
			nameOfLevel[140] = "141. Two Part Timing";
			nameOfLevel[141] = "142. Fly Through";
			nameOfLevel[142] = "143. Fly Through Part Two";
			nameOfLevel[143] = "144. Fly Through Part Three";
			nameOfLevel[144] = "145. One Step At A Time";
			nameOfLevel[145] = "146. Headbutt";
			nameOfLevel[146] = "147. Headbutt Part Two";
			nameOfLevel[147] = "148. Hiding from View";
			nameOfLevel[148] = "149. Hiding from View Part Two";
			nameOfLevel[149] = "150. Corridor 151";
			// Chapter 4
			nameOfLevel[150] = "151. Sticky";
			nameOfLevel[151] = "152. If You Cant Go Through";
			nameOfLevel[152] = "153. Shoot The J";
			nameOfLevel[153] = "154. Side Drop";
			nameOfLevel[154] = "155. Up And Around";
			nameOfLevel[155] = "156. Cells Part Three";
			nameOfLevel[156] = "157. Cling Jump";
			nameOfLevel[157] = "158. Hold Tight";
			nameOfLevel[158] = "159. Cake";
			nameOfLevel[159] = "160. Down Up";
			nameOfLevel[160] = "161. Down Up Part Two";
			nameOfLevel[161] = "162. Down Up Part Three";
			nameOfLevel[162] = "163. Through And Through";
			nameOfLevel[163] = "164. In The Way";
			nameOfLevel[164] = "165. Up And Over";
			nameOfLevel[165] = "166. Up And Over Part Two";
			nameOfLevel[166] = "167. Slow Down";
			nameOfLevel[167] = "168. Cake Part Two";
			nameOfLevel[168] = "169. One Way";
			nameOfLevel[169] = "170. One Way Part Two";
			nameOfLevel[170] = "171. Bullseye";
			nameOfLevel[171] = "172. Shatter";
			nameOfLevel[172] = "173. Keen Eye";
			nameOfLevel[173] = "174. Keen Eye Part Two";
			nameOfLevel[174] = "175. Leap Of Faith Part Three";
			nameOfLevel[175] = "176. The Other Side";
			nameOfLevel[176] = "177. Cling To What You Cant See";
			nameOfLevel[177] = "178. Blinded";
			nameOfLevel[178] = "179. Quick Reactions";
			nameOfLevel[179] = "180. Dreaming Of SK";
			nameOfLevel[180] = "181. You're Not The Only One";
			nameOfLevel[181] = "182. You're Not The Only One Part Two";
			nameOfLevel[182] = "183. Jamming";
			nameOfLevel[183] = "184. My Stop";
			nameOfLevel[184] = "185. My Stop Part Two";
			nameOfLevel[185] = "186. Pick A Side";
			nameOfLevel[186] = "187. Pick A Side Part Two";
			nameOfLevel[187] = "188. Catapult";
			nameOfLevel[188] = "189. Freedom?";
			// Chapter 5
			nameOfLevel[189] = "!(). Qb";
			nameOfLevel[190] = "!(!. lbh";
			nameOfLevel[191] = "!(@. erzrzore";
			nameOfLevel[192] = "!(#. gung";
			nameOfLevel[193] = "!($. lbh";
			nameOfLevel[194] = "!(%. xvyyrq";
			nameOfLevel[195] = "!({. uvz?";
			nameOfLevel[196] = "!(}. Bs";
			nameOfLevel[197] = "!(*. pbhefr";
			nameOfLevel[198] = "!((. lbh";
			nameOfLevel[199] = "@)). qba'g...";
			// Chapter 6
			nameOfLevel[200] = "201. The";
			nameOfLevel[201] = "202. Last";
			nameOfLevel[202] = "203. Few";
			nameOfLevel[203] = "204. Steps";
			nameOfLevel[204] = "205. Escape?";


			// Dark World
			nameOfLevel[205] = "-1. Back Again";
			nameOfLevel[206] = "-2. Learning The Ropes";
			nameOfLevel[207] = "-3. The Gap";
			nameOfLevel[208] = "-4. Danger, Don't Touch!";
			nameOfLevel[209] = "-5. Getting Through";
			nameOfLevel[210] = "-6. The Gate";
			nameOfLevel[211] = "-7. The Gate Part Two";
			nameOfLevel[212] = "-8. Distance";
			nameOfLevel[213] = "-9. Distance Part Two";
			nameOfLevel[214] = "-10. Putting It All Together";
			nameOfLevel[215] = "-11. Separation";
			nameOfLevel[216] = "-12. The Immovable Object";
			nameOfLevel[217] = "-13. The Music Is";
			nameOfLevel[218] = "-14. The Music Is Part Two";
			nameOfLevel[219] = "-15. Light Tap";
			nameOfLevel[220] = "-16. Sticky Button";
			nameOfLevel[221] = "-17. Falling Through";
			nameOfLevel[222] = "-18. Hop, Skip, and a Jump";
			nameOfLevel[223] = "-19. Long Jump";
			nameOfLevel[224] = "-20. Against The Wall";
			nameOfLevel[225] = "-21. If You Can't Get To It...";
			nameOfLevel[226] = "-22. Maze";
			nameOfLevel[227] = "-23. Break In";
			nameOfLevel[228] = "-24. Break Out";
			nameOfLevel[229] = "-25. Please Don't Tap Glass";
			nameOfLevel[230] = "-26. Brave New World";
			nameOfLevel[231] = "-27. Double Drop";
			nameOfLevel[232] = "-28. Now Do It While They're Watching";
			nameOfLevel[233] = "-29. Pushing Through";
			nameOfLevel[234] = "-30. Brave New World Part Two";
			nameOfLevel[235] = "-31. Timed Drop";
			nameOfLevel[236] = "-32. Jumping Through";
			nameOfLevel[237] = "-33. Blockage";
			nameOfLevel[238] = "-34. All Seeing";
			nameOfLevel[239] = "-35. All knowing";
			nameOfLevel[240] = "-36. Wheel";
			nameOfLevel[241] = "-37. 40,000 Volts";
			nameOfLevel[242] = "-38. Tic-Tock";
			nameOfLevel[243] = "-39. Tic-Tock Part Two";
			nameOfLevel[244] = "-40. Tic-Tock Part Three";
			nameOfLevel[245] = "-41. Man Mode";
			nameOfLevel[246] = "-42. Monkey Mode";
			nameOfLevel[247] = "-43. Head Low";
			nameOfLevel[248] = "-44. Fast Feet";
			nameOfLevel[249] = "-45. Fast Feet, Head Low";
			nameOfLevel[250] = "-46. Timing";
			nameOfLevel[251] = "-47. Timing Part Two";
			nameOfLevel[252] = "-48. Restricted Access";
			nameOfLevel[253] = "-49. Control Room";
			nameOfLevel[254] = "-50. Tunnel 51";
			// Chapter 2
			nameOfLevel[255] = "-51. New Moves";
			nameOfLevel[256] = "-52. Acrobatics";
			nameOfLevel[257] = "-53. Out of Reach";
			nameOfLevel[258] = "-54. Height";
			nameOfLevel[259] = "-55. Hot Potato";
			nameOfLevel[260] = "-56. Multi-Purpose Platform";
			nameOfLevel[261] = "-57. O.H.L.O";
			nameOfLevel[262] = "-58. Mobile Threat";
			nameOfLevel[263] = "-59. Send It Down";
			nameOfLevel[264] = "-60. Drop It";
			nameOfLevel[265] = "-61. Hop It";
			nameOfLevel[266] = "-62. Fatal Attraction";
			nameOfLevel[267] = "-63. Rolling";
			nameOfLevel[268] = "-64. Sliding Up";
			nameOfLevel[269] = "-65. Rock And A Hard Place";
			nameOfLevel[270] = "-66. Slipping Through The Cracks";
			nameOfLevel[271] = "-67. Turn At The End Of The Hall";
			nameOfLevel[272] = "-68. Up And Up";
			nameOfLevel[273] = "-69. Key Hole";
			nameOfLevel[274] = "-70. Opening Paths";
			nameOfLevel[275] = "-71. Climbing Up";
			nameOfLevel[276] = "-72. Going Down, Going Up";
			nameOfLevel[277] = "-73. Leap Of Faith";
			nameOfLevel[278] = "-74. Leap Of Faith Part Two";
			nameOfLevel[279] = "-75. Leap Of Faith Part Three";
			nameOfLevel[280] = "-76. Eye From Above";
			nameOfLevel[281] = "-77. Switched Around";
			nameOfLevel[282] = "-78. Rolling Part Two";
			nameOfLevel[283] = "-79. Sliding Up Part Two";
			nameOfLevel[284] = "-80. Single Edged Sword";
			nameOfLevel[285] = "-81. Double Edged Sword";
			nameOfLevel[286] = "-82. The Disappearing Wall";
			nameOfLevel[287] = "-83. The Disappearing Wall Part Two";
			nameOfLevel[288] = "-84. The Disappearing Wall Part Three";
			nameOfLevel[289] = "-85. The Disappearing Wall Part Four";
			nameOfLevel[290] = "-86. Turning With Your Eyes Closed";
			nameOfLevel[291] = "-87. Faith";
			nameOfLevel[292] = "-88. Knowledge";
			nameOfLevel[293] = "-89. Using Your Surroundings";
			nameOfLevel[294] = "-90. Blind";
			nameOfLevel[295] = "-91. Blind Part Two";
			nameOfLevel[296] = "-92. Blind Part Three";
			nameOfLevel[297] = "-93. Blind Part Four";
			nameOfLevel[298] = "-94. Back Again, Again";
			nameOfLevel[299] = "-95. Lost";
			nameOfLevel[300] = "-96. Division";
			nameOfLevel[301] = "-97. Elevator";
			nameOfLevel[302] = "-98. Elevator Part Two";
			nameOfLevel[303] = "-99. Elevator Part Three";
			nameOfLevel[304] = "-100 Stairway 101";
			// Chapter -3
			nameOfLevel[305] = "-101. Double Jump";
			nameOfLevel[306] = "-102. The Ditch";
			nameOfLevel[307] = "-103. Slide";
			nameOfLevel[308] = "-104. Further Distance";
			nameOfLevel[309] = "-105. The Ditch Part Two";
			nameOfLevel[310] = "-106. Getting Out";
			nameOfLevel[311] = "-107. G.O.H.L.O";
			nameOfLevel[312] = "-108. Avoid";
			nameOfLevel[313] = "-109. Crawling Up";
			nameOfLevel[314] = "-110. Crawling Up Part Two";
			nameOfLevel[315] = "-111. SMB";
			nameOfLevel[316] = "-112. Steps Up";
			nameOfLevel[317] = "-113. Upside-Down";
			nameOfLevel[318] = "-114. Upside-Down Part Two";
			nameOfLevel[319] = "-115. Falling Jump";
			nameOfLevel[320] = "-116. Momentum";
			nameOfLevel[321] = "-117. Momentum Part Two";
			nameOfLevel[322] = "-118. Down And Up";
			nameOfLevel[323] = "-119. Right And Up";
			nameOfLevel[324] = "-120. Conduction";
			nameOfLevel[325] = "-121. Hold Tight";
			nameOfLevel[326] = "-122. Out Of Sync";
			nameOfLevel[327] = "-123. Out Of Sync Part Two";
			nameOfLevel[328] = "-124. Around The World";
			nameOfLevel[329] = "-125. Time";
			nameOfLevel[330] = "-126. Time Part Two";
			nameOfLevel[331] = "-127. Needed Speed";
			nameOfLevel[332] = "-128. Here And Gone";
			nameOfLevel[333] = "-129. Here And Gone Part Two";
			nameOfLevel[334] = "-130. Here And Gone Part Three";
			nameOfLevel[335] = "-131. Air Time";
			nameOfLevel[336] = "-132. Riding The Rim";
			nameOfLevel[337] = "-133. Riding The Rim Part Two";
			nameOfLevel[338] = "-134. Blink";
			nameOfLevel[339] = "-135. Two Block";
			nameOfLevel[340] = "-136. Hopeful Timing";
			nameOfLevel[341] = "-137. Stop and Go";
			nameOfLevel[342] = "-138. Cells";
			nameOfLevel[343] = "-139. Cells Part Two";
			nameOfLevel[344] = "-140. One Hop At A Time";
			nameOfLevel[345] = "-141. Two Part Timing";
			nameOfLevel[346] = "-142. Fly Through";
			nameOfLevel[347] = "-143. Fly Through Part Two";
			nameOfLevel[348] = "-144. Fly Through Part Three";
			nameOfLevel[349] = "-145. One Step At A Time";
			nameOfLevel[350] = "-146. Headbutt";
			nameOfLevel[351] = "-147. Headbutt Part Two";
			nameOfLevel[352] = "-148. Hiding from View";
			nameOfLevel[353] = "-149. Hiding from View Part Two";
			nameOfLevel[354] = "-150. Corridor 151";
			// Chapter 4
			nameOfLevel[355] = "-151. Sticky";
			nameOfLevel[356] = "-152. If You Cant Go Through";
			nameOfLevel[357] = "-153. Shoot The J";
			nameOfLevel[358] = "-154. Side Drop";
			nameOfLevel[359] = "-155. Up And Around";
			nameOfLevel[360] = "-156. Cells Part Three";
			nameOfLevel[361] = "-157. Cling Jump";
			nameOfLevel[362] = "-158. Hold Tight";
			nameOfLevel[363] = "-159. Cake";
			nameOfLevel[364] = "-160. Down Up";
			nameOfLevel[365] = "-161. Down Up Part Two";
			nameOfLevel[366] = "-162. Down Up Part Three";
			nameOfLevel[367] = "-163. Through And Through";
			nameOfLevel[368] = "-164. In The Way";
			nameOfLevel[369] = "-165. Up And Over";
			nameOfLevel[370] = "-166. Up And Over Part Two";
			nameOfLevel[371] = "-167. Slow Down";
			nameOfLevel[372] = "-168. Cake Part Two";
			nameOfLevel[373] = "-169. One Way";
			nameOfLevel[374] = "-170 One Way Part Two";
			nameOfLevel[375] = "-171. Bullseye";
			nameOfLevel[376] = "-172. Shatter";
			nameOfLevel[377] = "-173. Keen Eye";
			nameOfLevel[378] = "-174. Keen Eye Part Two";
			nameOfLevel[379] = "-175. Leap Of Faith Part Three";
			nameOfLevel[380] = "-176. The Other Side";
			nameOfLevel[381] = "-177. Cling To What You Cant See";
			nameOfLevel[382] = "-178. Blinded";
			nameOfLevel[383] = "-179. Quick Reactions";
			nameOfLevel[384] = "-180. Black Star";
			nameOfLevel[385] = "-181. Your Not The Only One";
			nameOfLevel[386] = "-182. Your Not The Only One Part Two";
			nameOfLevel[387] = "-183. Jamming";
			nameOfLevel[388] = "-184. My Stop";
			nameOfLevel[389] = "-185. My Stop Part Two";
			nameOfLevel[390] = "-186. Pick A Side";
			nameOfLevel[391] = "-187. Pick A Side Part Two";
			nameOfLevel[392] = "-188. Catapult";
			nameOfLevel[393] = "-189. Freedom?";
			// Chapter 5
			nameOfLevel[394] = "-!(). V";
			nameOfLevel[395] = "-!(!. guvax";
			nameOfLevel[396] = "-!(@. lbh";
			nameOfLevel[397] = "-!(#. ner";
			nameOfLevel[398] = "-!($. ernql";
			nameOfLevel[399] = "-!(%. gb";
			nameOfLevel[400] = "-!({. snpr";
			nameOfLevel[401] = "-!(}. jung";
			nameOfLevel[402] = "-!(*. lbh";
			nameOfLevel[403] = "-!((. unir";
			nameOfLevel[404] = "-@)). qbar";
			// Chapter 6
			nameOfLevel[405] = "-201. The";
			nameOfLevel[406] = "-202. Last";
			nameOfLevel[407] = "-203. Few";
			nameOfLevel[408] = "-204. Steps";
			nameOfLevel[409] = "-205. Escape";
        }
    }
}
