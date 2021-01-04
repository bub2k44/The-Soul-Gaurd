using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPatrolState : PatrolState, IDogState
{
    private Dog _dog;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog.isPatrolState = true;
        _dog._navMeshAgent.speed = _dog.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _dog.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _dog.Destination();
        _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _dog.ChangeState(new DogIdleState());
        }
    }
}
