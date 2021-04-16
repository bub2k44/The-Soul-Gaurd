using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraSwitch : MonoBehaviour
{
    public GameObject ui, pauseMenu, restartButton, flyCamera, player;
    GameObject boss;
    public ChaseScript chaseAI;
    BossAIMovement bossAI;
    public NavMeshAgent navMesh;
    public Behaviour bossAnimator, wolfAnimator, rabbitAnimator;
    public ThirdPersonMovement thirdPersonMovement;
    ChaseScript chaseScript;
    public Transform chaseCam, playerCam;
    
    
    void Start()
    {
        flyCamera.SetActive(false);
        boss = GameObject.Find("BigBossAI");
        bossAI = boss.GetComponent<BossAIMovement>();
        chaseScript = boss.GetComponent<ChaseScript>();
    }

  
    void Update()
    {
        FreeFly();
        OnGUI();
    }
    void FreeFly()
    {
        if (Input.GetButtonDown("camera2") && (flyCamera.activeSelf == false))
        {
            flyCamera.SetActive(true);
            ui.SetActive(false);
            bossAnimator.enabled = false;
            thirdPersonMovement.enabled = false;
            if (chaseScript.follow == true)
            {
                chaseAI.enabled = false;
                navMesh.enabled = false;
                rabbitAnimator.enabled = false;
                flyCamera.transform.position = chaseCam.position;
                flyCamera.transform.rotation = chaseCam.rotation;

            }
            if (chaseScript.follow == false)
            {
                bossAI.enabled = false;
                wolfAnimator.enabled = false;
                flyCamera.transform.position = playerCam.position;
                flyCamera.transform.LookAt(player.transform);
            }
        }
        else if (Input.GetButtonDown("camera2") && (flyCamera.activeSelf == true))
        {
            flyCamera.SetActive(false);
            bossAnimator.enabled = true;
            thirdPersonMovement.enabled = true;
            ui.SetActive(true);
            if (chaseScript.follow == true)
            {
                chaseAI.enabled = true;
                navMesh.enabled = true;
                rabbitAnimator.enabled = true;
            }
            if (chaseScript.follow == false)
            {
                bossAI.enabled = true;
                wolfAnimator.enabled = true;
            }
        }
    }
    void OnGUI()
    {
        if (pauseMenu.activeSelf == true || restartButton.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
