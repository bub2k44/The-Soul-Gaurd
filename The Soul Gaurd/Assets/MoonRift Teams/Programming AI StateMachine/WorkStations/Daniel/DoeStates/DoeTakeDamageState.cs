using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeTakeDamageState : TakeDamageState, IDoeState
{
    private Doe _doe;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _doe.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _doe.TakeDamage(5);
            _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_HIT_FRONT;
            _tookDamage = true;
        }
        else
        {
            _doe.ChangeState(new DoeIdleState());
        }
    }
}
