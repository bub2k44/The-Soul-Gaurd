using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeDrinkState : DrinkState, IDoeState
{
    private Doe _doe;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe.isDrinkState = true;
    }

    public override void Exit()
    {
        _doe.thirstDuration = _doe.animalStats.thirstDuration;
        _doe.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _doe.ChangeState(new DoePatrolState());
        }
    }
}
