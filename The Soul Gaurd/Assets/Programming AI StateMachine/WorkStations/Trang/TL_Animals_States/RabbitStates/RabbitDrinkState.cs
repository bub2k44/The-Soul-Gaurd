using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitDrinkState : DrinkState, IRabbitState
{
    private Rabbit _rabbit;

    public void Enter(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _rabbit.isDrinkState = true;
    }

    public override void Exit()
    {
        _rabbit.thirstDuration = _rabbit.animalStats.thirstDuration;
        _rabbit.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _rabbit.ChangeState(new RabbitPatrolState());
        }
    }
}
