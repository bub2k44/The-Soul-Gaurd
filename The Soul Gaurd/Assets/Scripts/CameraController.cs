using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera freeFlyCam;
    public CinemachineFreeLook thirdPersonCam;
    public CinemachineVirtualCamera dollyCam;
    FreeFlyCamera flyCamera;
    public GameObject needs;
    public GameObject player;



    private void Start()
    {
        flyCamera.enabled = false;
    }

    public void Update()
    {
        //CameraSwitch();
        FlyingCamera();
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }
    }
    /*public void CameraSwitch()
    {
        if (Input.GetButtonDown("camera1") && isFirstPerson == false && flyCamera.enabled == false)
        {
            thirdPersonCam.m_Priority = 1;// changes priority of cam
            isFirstPerson = true;
            firstPersonScript.enabled = true;
            firstPersonLook.enabled = true;
            thirdPersonScript.enabled = false;
        }
        else if (Input.GetButtonDown("camera1") && isFirstPerson == true && flyCamera.enabled == false)
        {
            thirdPersonCam.m_Priority = 3;
            isFirstPerson = false;
            firstPersonScript.enabled = false;
            firstPersonLook.enabled = false;
            thirdPersonScript.enabled = true;   
        }
    }*/
    public void FlyingCamera()
    {
        if (Input.GetButtonDown("camera2"))
        {
            flyCamera.enabled = true;
            freeFlyCam.m_Priority = 2;
            player.SetActive(false);
            needs.SetActive(false);
            
        }
        else if (Input.GetButtonDown("camera2") && flyCamera == true)
        {
            player.SetActive(true);
            needs.SetActive(true);
            freeFlyCam.m_Priority = 0;
            flyCamera.enabled = false;
        }
    }
}

