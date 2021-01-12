using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //GameObject doorFire;
    // Start is called before the first frame update
    void Start()
    {
        //doorFire = GameObject.Find("Door_Fire");//Switch_Fire

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (Transform child in doorFire.transform)
        //{
        //    if (allFireLit())
        //    {
        //        child.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        child.gameObject.SetActive(false);
        //    }
        //}

    }

    public void TriggerFire()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(true);
                break;
            }
        }
    }

    //bool allFireLit()
    //{
    //    foreach (Transform child in transform)
    //    {
    //        if (!child.gameObject.activeSelf)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
}
