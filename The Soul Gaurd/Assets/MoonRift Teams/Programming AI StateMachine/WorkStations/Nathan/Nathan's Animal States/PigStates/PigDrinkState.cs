using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigDrinkState : DrinkState, IPigState
{
    private Pig _pig;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig.isDrinkState = true;
    }

    public override void Exit()
    {
        _pig.thirstDuration = _pig.animalStats.thirstDuration;
        _pig.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _pig.ChangeState(new PigPatrolState());
        }
    }
}