using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeChaseState : ChaseState, IDoeState
{
    private Doe _doe;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe._navMeshAgent.speed = _doe.animalStats.chaseSpeed;
        _doe.isChaseState = true;
    }

    public override void Exit()
    {
        _doe.isChaseState = false;
    }

    protected override void Chase()
    {
        _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_RUN;
    }
}
