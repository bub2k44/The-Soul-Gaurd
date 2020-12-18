using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxDrinkState : DrinkState, IFoxState
{
    private Fox _fox;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox.isDrinkState = true;
    }

    public override void Exit()
    {
        _fox.thirstDuration = _fox.animalStats.thirstDuration;
        _fox.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _fox.ChangeState(new FoxPatrolState());
        }
    }
}
