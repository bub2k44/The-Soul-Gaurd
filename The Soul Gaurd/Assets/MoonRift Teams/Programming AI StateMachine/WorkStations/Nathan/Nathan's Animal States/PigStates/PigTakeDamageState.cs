using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigTakeDamageState : TakeDamageState, IPigState
{
    private Pig _pig;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _pig.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _pig.TakeDamage(5);
            _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_HIT_FRONT;
            _tookDamage = true;
        }
        else
        {
            _pig.ChangeState(new PigIdleState());
        }
    }
}