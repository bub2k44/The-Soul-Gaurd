using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagDrinkState : DrinkState, IStagState
{
    private Stag _stag;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag.isDrinkState = true;
    }

    public override void Exit()
    {
        _stag.thirstDuration = _stag.animalStats.thirstDuration;
        _stag.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.EAT_2;

        if (_drinkTimer >= _drinkDuration)
        {
            _stag.ChangeState(new StagPatrolState());
        }
    }
}
