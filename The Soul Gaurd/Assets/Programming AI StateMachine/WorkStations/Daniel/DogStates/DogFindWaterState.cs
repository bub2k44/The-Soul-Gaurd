using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFindWaterState : FindWaterState, IDogState
{
    private Dog _dog;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog.isFindWaterState = true;
        _dog._navMeshAgent.speed = _dog.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _dog.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _dog.FindWater();
        _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_WALK;

        if (_dog._navMeshAgent.remainingDistance <= 0f)
        {
            _dog.ChangeState(new DogDrinkState());
        }
    }
}
