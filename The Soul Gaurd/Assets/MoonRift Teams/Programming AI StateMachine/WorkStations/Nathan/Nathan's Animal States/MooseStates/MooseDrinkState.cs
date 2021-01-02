using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseDrinkState : DrinkState, IMooseState
{
    private Moose _moose;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose.isDrinkState = true;
    }

    public override void Exit()
    {
        _moose.thirstDuration = _moose.animalStats.thirstDuration;
        _moose.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_EAT_1;

        if (_drinkTimer >= _drinkDuration)
        {
            _moose.ChangeState(new MoosePatrolState());
        }
    }
}