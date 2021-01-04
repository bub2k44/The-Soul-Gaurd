using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxTakeDamageState : TakeDamageState, IFoxState
{
    private Fox _fox;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _fox.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _fox.TakeDamage(5);
            _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_HIT_FRONT;
            _tookDamage = true;
        }
        else
        {
            _fox.ChangeState(new FoxIdleState());
        }
    }
}
