using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    public Transform spawnPosition, spawnPosition2, player, naaglos;
    public GameObject chaseCollider;
    public BossAIMovement ai;
    public ChaseScript chase;
    public ThirdPersonMovement playerScript;
    
    int checkpoint;
    public void Start()
    {
       // PlayerPrefs.SetInt("CheckPoint",0 );
        checkpoint = PlayerPrefs.GetInt("CheckPoint" );
        if(checkpoint == 0)
        {
            Debug.Log("Restart Level");
            RestartChase();
        }
        else if(checkpoint == 1)
        {
            Debug.Log("Fight");
            RestartFight();
        }
    }
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
        Debug.Log("Fight set");
        naaglos.position = spawnPosition.position;// Transforms boss to spawn point
        player.position = spawnPosition2.position;//transforms player to spawn point 2
        ai.enabled = true;//enables AI script
        chase.enabled = false;//disables chase script
        chase.nav.enabled = false;
        playerScript.fightCollider.SetActive(true);
        playerScript.arenaReached = true;
        playerScript.rabbitCam.m_Priority = 0;
        playerScript.wolfCam.m_Priority = 1;
        playerScript.rabbit.SetActive(false);
        playerScript.wolf.SetActive(true);
        Destroy(chaseCollider);
    }
    public void RestartChase()
    {
        Debug.Log("Chase set");
        naaglos.position = new Vector3(916, 3.1f, 137.2f);// Transforms boss to spawn point
        player.position = new Vector3(901.2f, 4.84f, 137.9f);//transforms player to spawn point 2
        playerScript.fightCollider.SetActive(false);
        ai.enabled = false;//enables AI script
        chase.enabled = true;//disables chase script
    }
    public void Menu()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
