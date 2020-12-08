using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Animal
{
    protected override void Look()
    {
        base.Look();
        RaycastHit hit;

        if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward, out hit, animalStats.lookRadius) && hit.collider.CompareTag("Sheep"))
        {
            chaseTarget = hit.transform;
            FindTarget(chaseTarget);
        }
        if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward, out hit, animalStats.lookRadius) && hit.collider.CompareTag("Doe"))
        {
            chaseTarget = hit.transform;
            FindTarget(chaseTarget);
        }
    }
    //protected override void OnTriggerEnter(Collider other)
    //{
    //    //base.OnTriggerEnter(other);
    //    if (other.gameObject.CompareTag("Bear"))
    //    {
    //        target = other.gameObject;
    //        ChangeState(new PlayState());
    //    }
    //}

    //protected override void OnTriggerExit(Collider other)
    //{
    //    //base.OnTriggerExit(other);


    //}
}
