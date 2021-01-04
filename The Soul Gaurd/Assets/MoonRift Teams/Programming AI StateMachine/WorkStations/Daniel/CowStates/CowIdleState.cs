using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowIdleState : IdleState, ICowState
{
    private Cow _cow;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow.isIdleState = true;
        _cow._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _cow.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_IDLE;
        }
        else
        {
            _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_IDLE_SEARCH;
        }

        if (_idleTimer >= _idleDuration)
        {
            _cow.ChangeState(new CowPatrolState());
        }
    }
}
