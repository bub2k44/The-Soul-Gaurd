using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagTakeDamageState : TakeDamageState, IStagState
{
    private Stag _stag;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _stag.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _stag.TakeDamage(5);
            _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.HIT_LEFT;
            _tookDamage = true;
        }
        else
        {
            _stag.ChangeState(new StagIdleState());
        }
    }
}
