using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDrinkState : DrinkState, IWolfState
{
    private Wolf _wolf;

    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf.isDrinkState = true;
    }

    public override void Exit()
    {
        _wolf.thirstDuration = _wolf.animalStats.thirstDuration;
        _wolf.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _wolf.ChangeState(new WolfPatrolState());
        }
    }
}