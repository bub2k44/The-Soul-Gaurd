using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogChaseState : ChaseState, IDogState
{
    private Dog _dog;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog._navMeshAgent.speed = _dog.animalStats.chaseSpeed;
        _dog.isChaseState = true;
    }

    public override void Exit()
    {
        _dog.isChaseState = false;
    }

    protected override void Chase()
    {
        _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_RUN;
    }
}
