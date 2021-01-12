using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitch : MonoBehaviour
{
    public bool switchOn = false;
    GameObject fires;

    // Start is called before the first frame update
    void Start()
    {
        fires = GameObject.Find("Switch_Fire");
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!switchOn)
            {
                Fire f = fires.GetComponent<Fire>();
                f.TriggerFire();
                switchOn = true;
                Debug.Log("Switch Triggered");
            }

        }
    }
}
