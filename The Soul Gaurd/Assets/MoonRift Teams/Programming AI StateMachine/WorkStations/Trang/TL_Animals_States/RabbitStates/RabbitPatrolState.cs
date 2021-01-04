using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitPatrolState : PatrolState, IRabbitState
{
    private Rabbit _rabbit;
    
    public void Enter(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _rabbit.isPatrolState = true;
        _rabbit._navMeshAgent.speed = _rabbit.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _rabbit.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _rabbit.Destination();
        _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _rabbit.ChangeState(new RabbitIdleState());
        }
    }
}
