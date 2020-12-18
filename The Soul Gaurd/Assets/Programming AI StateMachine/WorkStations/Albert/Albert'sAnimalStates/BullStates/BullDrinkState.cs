using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDrinkState : DrinkState, IBullState
{
   private Bull _bull;

    public void Enter(Bull bull)
    {
        _bull = bull;
        _bull.isDrinkState = true;
    }

    public override void Exit()
    {
        _bull.thirstDuration = _bull.animalStats.thirstDuration;
        _bull.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _bull.ChangeState(new BullPatrolState());
        }
    }
}
