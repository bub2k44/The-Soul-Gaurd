using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAwakenState : AwakenState, IRabbitState
{
    private Rabbit _rabbit;

    public void Enter(Rabbit rabbit)
    {
        _rabbit= rabbit;
        _rabbit.isAwakenState = true;
    }

    public override void Exit()
    {
        _rabbit.awakeDuration = _rabbit.animalStats.awakeDuration;
        _rabbit.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_DEAD;
        //there doesn't exist end sleep animation for rabbit

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _rabbit.ChangeState(new RabbitIdleState());
        }
    }
}
