using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitDeathState : DeathState, IRabbitState
{
    private Rabbit _rabbit;

    public void Enter(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _rabbit.isDeathState = true;
        _rabbit._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _rabbit.isDeathState = false;
        _rabbit._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_DEAD;
    }
}
