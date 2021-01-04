using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseIdleState : IdleState, IMooseState
{
    private Moose _moose;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose.isIdleState = true;
        _moose._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _moose.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_IDLE_1;
        }
        else
        {
            _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_IDLE_2;
        }

        if (_idleTimer >= _idleDuration)
        {
            _moose.ChangeState(new MoosePatrolState());
        }
    }
}
