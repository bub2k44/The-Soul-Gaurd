using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Restart : MonoBehaviour
{
    public Transform player, boss;
    public Transform playerPoint, bossPoint, playerPoint2, bossPoint2;
    public BossAIMovement bossScript;
    public ChaseScript chaseScript;
    public GameObject chaseCollider;
    int checkpoint;

    public void Start()
    {
       //PlayerPrefs.SetInt("Checkpoint", 0);
        checkpoint = PlayerPrefs.GetInt("Checkpoint");

       
        if(checkpoint == 1)
        {
            RestartFromCheckPoint();
        }
       
    }
    public void Update()
    {

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

    public void RestartFromCheckPoint()
    {
        player.position = playerPoint.position;
        boss.position = bossPoint.position;
        chaseCollider.SetActive(false);
        bossScript.enabled = true;
        chaseScript.enabled = false;
    }
}
