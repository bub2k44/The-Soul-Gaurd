using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullIdleState : IdleState, IBullState
{
    private Bull _bull;

    public void Enter(Bull bull)
    {
        _bull = bull;
        _bull.isIdleState = true;
        _bull._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _bull.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_IDLE_1;
        }
        else
        {
            _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_IDLE_2;
        }

        if (_idleTimer >= _idleDuration)
        {
            _bull.ChangeState(new BullPatrolState());
        }
    }
}
