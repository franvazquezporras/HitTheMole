using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectorLevel : MonoBehaviour
{
    //Variables
    Button[] levelButtons;


    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Carga el numero de niveles desbloqueados,actualiza los textos de los botones de niveles e inhabilita              */
    /*             Los niveles que no esten desbloqueados aun                                                                        */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
        if (PlayerPrefs.GetInt("Level") >= 2)
            unlockedLevels = PlayerPrefs.GetInt("Level");

        levelButtons = new Button[transform.childCount];
        for (int actualLevel = 0; actualLevel < levelButtons.Length; actualLevel++)
        {
            levelButtons[actualLevel] = transform.GetChild(actualLevel).GetComponent<Button>();            
            if (actualLevel + 1 > unlockedLevels)
            {
                levelButtons[actualLevel].interactable = false;
                levelButtons[actualLevel].GetComponent<Transform>().GetChild(1).gameObject.SetActive(true);
            }
        }
    }


    /*********************************************************************************************************************************/
    /*Funcion: LoadLevel                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: Nivel a cargar                                                                                          */
    /*Descripción: guarda el nivel en el player pref,y carga la escena del nivel                                                     */
    /*********************************************************************************************************************************/
    public void LoadLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        Application.LoadLevel(level);
    }

    /*********************************************************************************************************************************/
    /*Funcion: LoadScene                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: nextLevel (nivel a cargar)                                                                              */
    /*Descripción: Carga la escena recibida                                                                                          */
    /*********************************************************************************************************************************/
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    /*********************************************************************************************************************************/
    /*Funcion: nextLevel                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Comprueba si el nivel siguiente esta desbloqueado, si no lo está desbloquea el mismo lo guarda en los niveles     */
    /*             desbloqueados                                                                                                     */
    /*********************************************************************************************************************************/
    public void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("UnlockedLevels"))
            PlayerPrefs.SetInt("UnlockedLevels", PlayerPrefs.GetInt("UnlockedLevels") + 1);       
    }
}
