using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSleepState : SleepState, IDogState
{
    private Dog _dog;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog.sleepyFX.Play();
        _dog.isSleepState = true;
        _dog._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _dog.sleepyFX.Stop();
        _dog.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_IDLE;

        if (_sleepTimer >= _sleepDuration)
        {
            _dog.ChangeState(new DogAwakenState());
        }
    }
}
