using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    //Variables
    [Header("Spawners")]
    [SerializeField] private Transform[] spawnsFloor;
    [SerializeField] private GameObject hole;
    [SerializeField] private int numberOfHoleLevel;
    private Transform[] activeSpawns;
    private int savedHoles;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Image musicOFF;
    
    [Header("Panel References")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject initPanel;
    [SerializeField] private Sprite goImage;

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Activa el juego y crea el array con el maximo de hoyos señalados en la variable                                   */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        Time.timeScale = 1;
        activeSpawns = new Transform[numberOfHoleLevel];
    }


    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Recorre todos los bloques que permitan spawn y de forma aleatora selecciona cuales tendran spawn y cuales no      */
    /*              si llega al final de la lista puede llegar o no al limite de hoyos                                               */
    /*********************************************************************************************************************************/
    private void Start()
    {
        savedHoles = 0;
        for(int block = 0; block < spawnsFloor.Length; block++)
        {
            int save = Random.Range(0, 2);         
            if (save == 1)
            {
                activeSpawns[savedHoles] = spawnsFloor[block];
                savedHoles++;
                if (savedHoles == numberOfHoleLevel)                                   
                    break;      
            }           
        }
        StartCoroutine(InitLevel());
        
    }

    /*********************************************************************************************************************************/
    /*Funcion: OpenPause                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Pausa el juego y muestra el menu de pause                                                                         */
    /*********************************************************************************************************************************/
    public void OpenPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }


    /*********************************************************************************************************************************/
    /*Funcion: ClosePause                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Cierra el menu de pause y reactiva el juego                                                                       */
    /*********************************************************************************************************************************/
    public void ClosePause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    /*********************************************************************************************************************************/
    /*Funcion: LosePanel                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Pausa el juego y muestra el panel de partida perdida                                                              */
    /*********************************************************************************************************************************/
    public void LosePanel()
    {
        Time.timeScale = 0;
        losePanel.SetActive(true);
    }

    /*********************************************************************************************************************************/
    /*Funcion: ClearLevel                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Pausa el juego y muestra el panel de nivel completado                                                             */
    /*********************************************************************************************************************************/
    public void ClearLevel()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);       
    }

    /*********************************************************************************************************************************/
    /*Funcion: MusicOnOff                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: actualiza el sonido master y mutea la musica o la desmutea                                                        */
    /*********************************************************************************************************************************/
    public void MusicOnOff()
    {
        if (musicOFF.IsActive())
        {
            audioMixer.SetFloat("masterVolume", 1);
            musicOFF.gameObject.SetActive(false);
        }
        else
        {
            audioMixer.SetFloat("masterVolume", -80);            
            musicOFF.gameObject.SetActive(true);
        }            
    }
    /*********************************************************************************************************************************/
    /*Funcion: Retry                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Recarga la escena actual                                                                                          */
    /*********************************************************************************************************************************/
    public void Retry()
    {
        LoadScene(SceneManager.GetActiveScene().name);
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
    /*Funcion: InitLevel                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Genera los hoyos de los topos y muestra el cartel de inicio de partida(ready +go)                                 */
    /*********************************************************************************************************************************/
    private IEnumerator InitLevel()
    {
       
        yield return new WaitForSeconds(2);
        initPanel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = goImage;       
        yield return new WaitForSeconds(1);
        initPanel.SetActive(false);
        for (int i = 0; i < savedHoles; i++)
            Instantiate(hole, new Vector3(activeSpawns[i].position.x, activeSpawns[i].position.y, activeSpawns[i].position.z), hole.transform.rotation * Quaternion.identity);
    }
}
