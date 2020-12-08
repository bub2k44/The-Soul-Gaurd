using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    

    //public void GoToSettingsMenu()
    //{

    //}

    //private SceneHandlerScript SceneHandler;
    void Start()
    {
        //SceneHandler = GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandlerScript>();
    }
    //public void PlayGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
    public void PlayGame()
    {
        levelLoader.LoadNextLevel();
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

}
