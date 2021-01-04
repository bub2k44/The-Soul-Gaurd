using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDeathState : DeathState,IBullState
{
    private Bull _bull;

    public void Enter(Bull bull)
    {
        _bull = bull;
        _bull.isDeathState = true;
        _bull._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _bull.isDeathState = false;
        _bull._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_DEAD;
    }
}
