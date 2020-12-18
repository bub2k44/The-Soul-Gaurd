using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDeathState : DeathState, IDogState
{
    private Dog _dog;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog.isDeathState = true;
        _dog._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _dog.isDeathState = false;
        _dog._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_IDLE_SITTING;
    }
}
