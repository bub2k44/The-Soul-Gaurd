using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    string name;
    GameObject doorGroup;

    void Start()
    {
        name = gameObject.name;
        doorGroup = GameObject.Find("Doors");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TriggerDoor()
    {
        GetDoors d = doorGroup.GetComponent<GetDoors>();

        switch (name)
        {
            case "Switch_1":
                d.TriggerDoors(1);
                break;
            case "Switch_2":
                d.TriggerDoors(1);
                d.TriggerDoors(4);
                break;
            case "Switch_3":
                d.TriggerDoors(2);
                d.TriggerDoors(3);
                break;
            case "Switch_4":
                d.TriggerDoors(3);
                break;
            default:
                Debug.Log("Second puzzle's switches not working");
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Switch Triggered");
            TriggerDoor();
        }
    }
}
