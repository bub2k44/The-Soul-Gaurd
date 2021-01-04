using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseTakeDamageState : TakeDamageState, IMooseState
{
    private Moose _moose;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _moose.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _moose.TakeDamage(5);
            _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_HIT_FRONT;
            _tookDamage = true;
        }
        else
        {
            _moose.ChangeState(new MooseIdleState());
        }
    }
}