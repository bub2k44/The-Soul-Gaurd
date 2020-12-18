using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxIdleState : IdleState, IFoxState
{
    private Fox _fox;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox.isIdleState = true;
        _fox._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _fox.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_IDLE;
        }
        else
        {
            _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_IDLE_ALT;
        }

        if (_idleTimer >= _idleDuration)
        {
            _fox.ChangeState(new FoxPatrolState());
        }
    }
}
