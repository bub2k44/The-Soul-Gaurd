using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterTakeDamageState : TakeDamageState, IRoosterState
{
    private Rooster _rooster;

    public void Enter(Rooster rooster)
    {
        _rooster= rooster;
        _rooster.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _rooster.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _rooster.TakeDamage(5);
            _rooster.roosterAnimationState= Rooster.ROOSTER_ANIMATIONSTATE.ARM_COCK_HIT;
            _tookDamage = true;
        }
        else
        {
            _rooster.ChangeState(new RoosterIdleState());
        }
    }
}
