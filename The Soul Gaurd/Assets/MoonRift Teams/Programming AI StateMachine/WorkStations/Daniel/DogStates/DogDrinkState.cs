using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDrinkState : DrinkState, IDogState
{
    private Dog _dog;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog.isDrinkState = true;
    }

    public override void Exit()
    {
        _dog.thirstDuration = _dog.animalStats.thirstDuration;
        _dog.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _dog.ChangeState(new DogPatrolState());
        }
    }
}
