using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [SerializeField] private Transform[] spawnsFloor;
    [SerializeField] private GameObject hole;
    [SerializeField] private int numberOfHoleLevel;
    private Transform[] activeSpawns;

    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Image musicOFF;

    private void Awake()
    {
        Time.timeScale = 1;
        activeSpawns = new Transform[numberOfHoleLevel];
    }
    private void Start()
    {
        int savedHoles = 0;
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
        for(int i = 0; i < activeSpawns.Length; i++)                       
                Instantiate(hole, new Vector3(activeSpawns[i].position.x, activeSpawns[i].position.y, activeSpawns[i].position.z),hole.transform.rotation*Quaternion.identity);        
    }

    public void OpenPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void ClosePause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void LosePanel()
    {
        losePanel.SetActive(true);
    }

    public void ClearLevel()
    {
        //mostar panel de level completo
        //cargar mapa de niveles o siguiente nivel
    }
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
    public void Retry()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);        
    }

    
}
