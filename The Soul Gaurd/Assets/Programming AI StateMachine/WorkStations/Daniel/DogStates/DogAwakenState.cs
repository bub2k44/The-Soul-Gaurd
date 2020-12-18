using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAwakenState : AwakenState, IDogState
{
    private Dog _dog;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog.isAwakenState = true;
    }

    public override void Exit()
    {
        _dog.awakeDuration = _dog.animalStats.awakeDuration;
        _dog.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_IDLE;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _dog.ChangeState(new DogIdleState());
        }
    }
}
