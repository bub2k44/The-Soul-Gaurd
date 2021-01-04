using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDrinkState : DrinkState, IBearState
{
    private Bear _bear;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear.isDrinkState = true;
    }

    public override void Exit()
    {
        _bear.thirstDuration = _bear.animalStats.thirstDuration;
        _bear.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _bear.ChangeState(new BearPatrolState());
        }
    }
}