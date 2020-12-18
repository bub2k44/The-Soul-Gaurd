using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepTakeDamageState : TakeDamageState, ISheepState
{
    private Sheep _sheep;

    public void Enter(Sheep sheep)
    {
        _sheep = sheep;
        _sheep.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _sheep.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _sheep.TakeDamage(5);
            _sheep.sheepAnimationState = Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_HIT_FRONT;
            _tookDamage = true;
        }
        else
        {
            _sheep.ChangeState(new SheepIdleState());
        }
    }
}
