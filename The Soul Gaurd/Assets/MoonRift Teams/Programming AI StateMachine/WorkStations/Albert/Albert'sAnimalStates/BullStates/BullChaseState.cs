using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullChaseState : ChaseState, IBullState
{
    private Bull _bull;

    public void Enter(Bull bull)
    {
        _bull = bull;
        _bull._navMeshAgent.speed = _bull.animalStats.chaseSpeed;
        _bull.isChaseState = true;
    }

    public override void Exit()
    {
        _bull.isChaseState = false;
    }

    protected override void Chase()
    {
        _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_RUN;
    }
}
