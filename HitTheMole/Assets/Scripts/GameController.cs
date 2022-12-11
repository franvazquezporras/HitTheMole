using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
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
    
    private void Awake()
    {
        Time.timeScale = 1;
        activeSpawns = new Transform[numberOfHoleLevel];
    }

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
        Time.timeScale = 0;
        losePanel.SetActive(true);
    }

    public void ClearLevel()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);       
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
    
    public void NextLevel(string nextLevel)
    {
        LoadScene(nextLevel);
    }

    public void LoadMap()
    {
        LoadScene("LevelMap");
    }
    
    public void Retry()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);        
    }

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
