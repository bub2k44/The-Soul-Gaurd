using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeIdleState : IdleState, IDoeState
{
    private Doe _doe;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe.isIdleState = true;
        _doe._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _doe.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_IDLE;
        }
        else
        {
            _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_IDLE_HEADSHAKE;
        }

        if (_idleTimer >= _idleDuration)
        {
            _doe.ChangeState(new DoePatrolState());
        }
    }
}
