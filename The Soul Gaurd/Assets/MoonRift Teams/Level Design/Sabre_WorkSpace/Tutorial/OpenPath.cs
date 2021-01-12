using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPath : MonoBehaviour
{
    GameObject door;

    void Start()
    {
        door = GameObject.Find("Door");
    }

    void Update()
    {


        if (checkIfAllOn())
        {
            Destroy(door);
        }
    }

    bool checkIfAllOn()
    {
        foreach (Transform child in transform)
        {
            PressureSwitch p = child.GetComponent<PressureSwitch>();
            if (p.switchOn == false)
            {
                return false;
            }
        }
        return true;
    }
}
