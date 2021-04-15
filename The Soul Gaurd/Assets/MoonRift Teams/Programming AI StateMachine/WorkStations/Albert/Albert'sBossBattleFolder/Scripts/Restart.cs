using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightShift))//take off before build
        {
           
                RestartLevel();
           
        }
    }
    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

  
    public void RestartFight()
    {
        SceneManager.LoadScene("Albert's Boss Fight Scene");
    }
    public void Menu()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
