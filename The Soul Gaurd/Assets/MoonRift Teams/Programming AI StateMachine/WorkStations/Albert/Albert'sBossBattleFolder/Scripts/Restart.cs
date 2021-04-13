using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    public Transform player, boss;
    public Transform playerPoint, bossPoint, playerPoint2, bossPoint2;
    public BossAIMovement bossScript;
    public ChaseScript chaseScript;
    public ThirdPersonMovement playerMovement;
    public GameObject chaseCollider;
    int checkpoint;

    public void Start()
    {
      
    }
    public void Update()
    {
        //checkpoint = PlayerPrefs.GetInt("Checkpoint");

        if (Input.GetKeyDown(KeyCode.RightShift))
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
