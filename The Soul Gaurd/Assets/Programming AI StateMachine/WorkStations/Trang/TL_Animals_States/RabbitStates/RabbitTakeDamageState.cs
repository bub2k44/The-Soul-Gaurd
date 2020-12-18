using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitTakeDamageState : TakeDamageState, IRabbitState
{
    private Rabbit _rabbit;

    public void Enter(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _rabbit.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _rabbit.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _rabbit.TakeDamage(5);
            _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_HIT_FRONT;
            _tookDamage = true;
        }
        else
        {
            _rabbit.ChangeState(new RabbitIdleState());
        }
    }
}
