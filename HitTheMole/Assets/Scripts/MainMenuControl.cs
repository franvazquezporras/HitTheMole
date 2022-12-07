using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuControl : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject OptionMenu;

    private void Start()
    {
        BackToMenu();
    }

    public void BackToMenu()
    {
        OptionMenu.SetActive(false);
        MainMenu.SetActive(true);

    }

    public void GoOptionMenu()
    {

        OptionMenu.SetActive(true);
        MainMenu.SetActive(false);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
