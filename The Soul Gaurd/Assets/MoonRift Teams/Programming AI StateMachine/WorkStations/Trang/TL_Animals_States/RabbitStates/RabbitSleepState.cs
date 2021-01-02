using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSleepState : SleepState, IRabbitState
{
    private Rabbit _rabbit;

    public void Enter(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _rabbit.sleepyFX.Play();
        _rabbit.isSleepState = true;
        _rabbit._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _rabbit.sleepyFX.Stop();
        _rabbit.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_IDLE_2;
        //there isnt an animation for sleep start

        if (_sleepTimer >= _sleepDuration)
        {
            _rabbit.ChangeState(new RabbitAwakenState());
        }
    }
}
