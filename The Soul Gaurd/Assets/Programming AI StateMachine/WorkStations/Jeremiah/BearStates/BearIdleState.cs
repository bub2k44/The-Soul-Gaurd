using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearIdleState : IdleState, IBearState
{
    private Bear _bear;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear.isIdleState = true;
        _bear._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _bear.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_IDLE;
        }
        else
        {
            _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_IDLE_SEARCH;
        }

        if (_idleTimer >= _idleDuration)
        {
            _bear.ChangeState(new BearPatrolState());
        }
    }
}