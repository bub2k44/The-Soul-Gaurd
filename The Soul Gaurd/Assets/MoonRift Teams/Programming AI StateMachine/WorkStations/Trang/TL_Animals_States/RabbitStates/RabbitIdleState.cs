using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitIdleState : IdleState, IRabbitState
{
    private Rabbit _rabbit;

    public void Enter(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _rabbit.isIdleState = true;
        _rabbit._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _rabbit.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_IDLE_1;
        }
        else
        {
            _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_IDLE_2;
        }
        

        if (_idleTimer >= _idleDuration)
        {
            _rabbit.ChangeState(new RabbitPatrolState());
        }
    }
}
