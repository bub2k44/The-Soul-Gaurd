using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitFindWaterState : FindWaterState, IRabbitState
{
    private Rabbit _rabbit;

    public void Enter(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _rabbit.isFindWaterState = true;
        _rabbit._navMeshAgent.speed = _rabbit.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _rabbit.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _rabbit.FindWater();
        _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_WALK;

        if (_rabbit._navMeshAgent.remainingDistance <= 0f)
        {
            _rabbit.ChangeState(new RabbitDrinkState());
        }
    }
}
