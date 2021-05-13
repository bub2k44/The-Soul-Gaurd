using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour
{
    public bool isMeleeAttacking;
    TargetScript parent;

    private void Start()
    {
        parent = GetComponentInParent<TargetScript>();

    }
    public void OnTriggerEnter(Collider other)
    {
        if(parent.team == TargetScript.Team.one)
        {
            if (other.gameObject.CompareTag("TeamTwo"))
            {
                isMeleeAttacking = true;
            }
        }
        if(parent.team == TargetScript.Team.two)
        {
            if (other.gameObject.CompareTag("TeamOne"))
            {
                isMeleeAttacking = true;
            }
        }
    }
}
