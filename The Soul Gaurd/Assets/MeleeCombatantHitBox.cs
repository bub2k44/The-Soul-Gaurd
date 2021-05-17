using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombatantHitBox : MonoBehaviour
{
    TargetScript parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<TargetScript>();
        if (parent.team == TargetScript.Team.one)
        {
            transform.gameObject.tag = "HitBox1";

        }
        if (parent.team == TargetScript.Team.two)
        {
            transform.gameObject.tag = "HitBox2";
        }
    }

    // Update is called once per frame
    void Update()
    {
      

    }


    public void OnTriggerEnter(Collider other)
    {
        if (parent.team == TargetScript.Team.one)
        {
            if (other.gameObject.CompareTag("HitBox2"))
            {
                parent.TakeDamage(30);
            }
        }
        if (parent.team == TargetScript.Team.two)
        {
            if (other.gameObject.CompareTag("HitBox1"))
            {
                Debug.Log("Hurt2");
                parent.TakeDamage(50);
            }
        }
    }
    
}
