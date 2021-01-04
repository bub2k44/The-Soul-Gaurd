using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfDrinkState : DrinkState, ICalfState
{
    private Calf _calf;

    public void Enter(Calf calf)
    {
        _calf = calf;
        _calf.isDrinkState = true;
    }

    public override void Exit()
    {
        _calf.thirstDuration = _calf.animalStats.thirstDuration;
        _calf.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.EAT_1;

        if (_drinkTimer >= _drinkDuration)
        {
            _calf.ChangeState(new CalfPatrolState());
        }
    }
}
