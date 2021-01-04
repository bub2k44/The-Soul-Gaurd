using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfIdleState : IdleState, ICalfState
{
    private Calf _calf;

    public void Enter(Calf calf)
    {
        _calf = calf;
        _calf.isIdleState = true;
        _calf._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _calf.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.IDLE_1;
        }
        else
        {
            _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.IDLE_2;
        }

        if (_idleTimer >= _idleDuration)
        {
            _calf.ChangeState(new CalfPatrolState());
        }
    }
}
