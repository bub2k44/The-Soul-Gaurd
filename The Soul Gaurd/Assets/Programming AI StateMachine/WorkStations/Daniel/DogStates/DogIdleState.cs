using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogIdleState : IdleState, IDogState
{
    private Dog _dog;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog.isIdleState = true;
        _dog._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _dog.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_IDLE;
        }
        else
        {
            _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_IDLE_SITTING;
        }

        if (_idleTimer >= _idleDuration)
        {
            _dog.ChangeState(new DogPatrolState());
        }
    }
}
