using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfChaseState : ChaseState, ICalfState
{
    private Calf _calf;

    public void Enter(Calf calf)
    {
        _calf = calf;
        _calf._navMeshAgent.speed = _calf.animalStats.chaseSpeed;
        _calf.isChaseState = true;
    }

    public override void Exit()
    {
        _calf.isChaseState = false;
    }

    protected override void Chase()
    {
        _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.RUN;
    }
}
