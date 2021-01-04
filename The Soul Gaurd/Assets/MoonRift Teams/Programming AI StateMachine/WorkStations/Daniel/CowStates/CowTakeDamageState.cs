using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowTakeDamageState : TakeDamageState, ICowState
{
    private Cow _cow;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _cow.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _cow.TakeDamage(5);
            _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_HIT;
            _tookDamage = true;
        }
        else
        {
            _cow.ChangeState(new CowIdleState());
        }
    }
}
