using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfTakeDamageState : TakeDamageState, IWolfState
{
    private Wolf _wolf;

    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _wolf.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {          
            _wolf.TakeDamage(5);
            _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_HIT_LEFT;
            _tookDamage = true;
        }
        else
        {
            _wolf.ChangeState(new WolfIdleState());
        }
    }
}