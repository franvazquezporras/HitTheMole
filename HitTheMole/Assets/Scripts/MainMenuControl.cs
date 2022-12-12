using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuControl : MonoBehaviour
{

    //Variables
    [Header("Menus")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject OptionMenu;


    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Carga el menu principal                                                                                           */
    /*********************************************************************************************************************************/
    private void Start()
    {
        BackToMenu();
    }
    /*********************************************************************************************************************************/
    /*Funcion: BackToMenu                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Carga el menu principal y oculta el de opciones                                                                   */
    /*********************************************************************************************************************************/
    public void BackToMenu()
    {
        OptionMenu.SetActive(false);
        MainMenu.SetActive(true);

    }
    /*********************************************************************************************************************************/
    /*Funcion: GoOptionMenu                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Carga el menu de opciones y oculta el menu principal                                                              */
    /*********************************************************************************************************************************/
    public void GoOptionMenu()
    {

        OptionMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    /*********************************************************************************************************************************/
    /*Funcion: ExitGame                                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Cierra el juego                                                                                                   */
    /*********************************************************************************************************************************/
    public void ExitGame()
    {
        Application.Quit();
    }

    /*********************************************************************************************************************************/
    /*Funcion: LoadScene                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: scene (escena a cargar                                                                                  */
    /*Descripci�n: Carga la escena recibida                                                                                          */
    /*********************************************************************************************************************************/
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
