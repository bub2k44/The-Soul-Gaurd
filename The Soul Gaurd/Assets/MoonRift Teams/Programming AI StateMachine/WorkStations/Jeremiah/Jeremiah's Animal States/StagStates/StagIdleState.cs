using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagIdleState : IdleState, IStagState
{
    private Stag _stag;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag.isIdleState = true;
        _stag._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _stag.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.IDLE_1;
        }
        else
        {
            _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.IDLE_2;
        }

        if (_idleTimer >= _idleDuration)
        {
            _stag.ChangeState(new StagPatrolState());
        }
    }
}
