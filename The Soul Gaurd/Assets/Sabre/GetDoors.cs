using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDoors : MonoBehaviour
{
    public void TriggerDoors(int door)
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains(door.ToString()))
            {
                if (child.gameObject.activeSelf == true)
                    child.gameObject.SetActive(false);
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
