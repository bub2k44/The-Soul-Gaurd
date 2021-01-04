using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    public BasicStats baseStats;
    public string Animal;
    public GameObject target = null;

    public void OnTriggerStay(Collider other)
    {
        if (target == null)
        {
            GameObject temp = other.gameObject.transform.parent.gameObject;
            Debug.Log(temp.tag + " " + temp.name);
            if (temp.tag == "Player")
            {
                if (temp.GetComponent<Player>().Target == null)
                {
                    temp.GetComponent<Player>().Target = gameObject;
                    target = temp.gameObject;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.gameObject == target)
        {
            target = null;
            other.gameObject.transform.parent.gameObject.GetComponent<Player>().Target = null;
        }
    }

    public void Possess()
    {
        Destroy(gameObject);
        GameObject temp = Instantiate(Resources.Load("Prefabs/" + Animal) as GameObject);
        temp.transform.position = transform.position;
        temp.transform.rotation = transform.rotation;
    }

    private GameObject holder;
    private void FixControl()
    {
        holder.GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = true;
    }
}
