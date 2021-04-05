using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
      // PlayerPrefs.SetInt("Checkpoint", 0);
        checkpoint = PlayerPrefs.GetInt("Checkpoint");

       
        if(checkpoint == 1)
        {
            player.position = playerPoint.position;
            boss.position = bossPoint.position;
            chaseCollider.SetActive(false);
            bossScript.enabled = true;
            chaseScript.enabled = false;
            playerMovement.rabbitCam.m_Priority = 0;
            playerMovement.wolfCam.m_Priority = 1;
            playerMovement.rabbit.SetActive(false);
            playerMovement.wolf.SetActive(true);
        }
      
    }
    public void Update()
    {
        checkpoint = PlayerPrefs.GetInt("Checkpoint");

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
           
                RestartLevel();
           
        }
    }
    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        Debug.Log(checkpoint);
    }

    public void RestartWholeLevel()
    {
        PlayerPrefs.SetInt("Checkpoint", 0);

        RestartLevel();
    }
    public void Checkpoint1()
    {
        PlayerPrefs.SetInt("Checkpoint", 1);
        RestartLevel();
    }
    public void RestartFromCheckPoint()
    {
        
    }
}
