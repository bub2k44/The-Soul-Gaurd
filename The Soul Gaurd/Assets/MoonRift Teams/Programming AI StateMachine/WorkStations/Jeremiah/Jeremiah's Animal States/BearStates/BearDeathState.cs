using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDeathState : DeathState, IBearState
{
    private Bear _bear;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear.isDeathState = true;
        _bear._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _bear.isDeathState = false;
        _bear._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {       
        _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_DEAD_SIDE;
    }
}