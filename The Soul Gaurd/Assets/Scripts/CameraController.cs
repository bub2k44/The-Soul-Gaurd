using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera firstPersonCam;
    public CinemachineVirtualCamera freeFlyCam;
    public CinemachineFreeLook thirdPersonCam;
    bool isFirstPerson = true;
    PlayerMovement firstPersonScript;
    MouseLook firstPersonLook;
    ThirdPersonMovement thirdPersonScript;
    FreeFlyCamera flyCamera;

    private void Start()
    {
        firstPersonLook = GameObject.Find("FirstPersonCam").GetComponent<MouseLook>();
        firstPersonScript = GameObject.Find("FirstPersonPlayer").GetComponent<PlayerMovement>();
        thirdPersonScript = GameObject.Find("ThirdPersonPlayer").GetComponent<ThirdPersonMovement>();
        flyCamera = GameObject.Find("FlyCam").GetComponent<FreeFlyCamera>();
        thirdPersonScript.enabled = true;
        firstPersonScript.enabled = false;
        flyCamera.enabled = false;
    }

    public void Update()
    {
        CameraSwitch();
        FlyingCamera();
    }
    public void CameraSwitch()
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
    }
    public void FlyingCamera()
    {
        if (Input.GetButtonDown("camera2") && flyCamera.enabled == false)
        {
            flyCamera.enabled = true;
            freeFlyCam.m_Priority = 4;
            firstPersonScript.enabled = false;
            firstPersonLook.enabled = false;
            thirdPersonScript.enabled = false;
        }
        else if (Input.GetButtonDown("camera2") && flyCamera == true)
        {
            freeFlyCam.m_Priority = 0;
            flyCamera.enabled = false;
            if(thirdPersonCam.m_Priority == 3)
            {
                thirdPersonScript.enabled = true;
            }
            else if (thirdPersonCam.m_Priority == 1)
            {
                firstPersonScript.enabled = true;
                firstPersonLook.enabled = true;
            }
        }
    }
}

