using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearChaseState : ChaseState ,IBearState
{
    private Bear _bear;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear._navMeshAgent.speed = _bear.animalStats.chaseSpeed;
        _bear.isChaseState = true;
    }

    public override void Exit()
    {
        _bear.isChaseState = false;
    }

    protected override void Chase()
    {
        _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_RUN;
    }
}