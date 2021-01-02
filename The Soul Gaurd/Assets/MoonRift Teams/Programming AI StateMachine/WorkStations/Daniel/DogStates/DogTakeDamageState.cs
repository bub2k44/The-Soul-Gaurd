using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTakeDamageState : TakeDamageState, IDogState
{
    private Dog _dog;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _dog.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _dog.TakeDamage(5);
            _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_BARK;
            _tookDamage = true;
        }
        else
        {
            _dog.ChangeState(new DogIdleState());
        }
    }
}
