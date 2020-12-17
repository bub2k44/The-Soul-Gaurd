using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTakeDamageState : TakeDamageState, IBearState
{
    private Bear _bear;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _bear.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _bear.TakeDamage(5);
            _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_HIT;
            _tookDamage = true;
        }
        else
        {
            _bear.ChangeState(new BearIdleState());
        }
    }
}