using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowDrinkState : DrinkState, ICowState
{
    private Cow _cow;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow.isDrinkState = true;
    }

    public override void Exit()
    {
        _cow.thirstDuration = _cow.animalStats.thirstDuration;
        _cow.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _cow.ChangeState(new CowPatrolState());
        }
    }
}
