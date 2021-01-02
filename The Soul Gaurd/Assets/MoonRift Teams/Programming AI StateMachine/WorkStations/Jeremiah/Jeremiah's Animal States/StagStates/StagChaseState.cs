using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagChaseState : ChaseState, IStagState
{
    private Stag _stag;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag._navMeshAgent.speed = _stag.animalStats.chaseSpeed;
        _stag.isChaseState = true;
    }

    public override void Exit()
    {
        _stag.isChaseState = false;
    }

    protected override void Chase()
    {
        _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.RUN;
    }
}
