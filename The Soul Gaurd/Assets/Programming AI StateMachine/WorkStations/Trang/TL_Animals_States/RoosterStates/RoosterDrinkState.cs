using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterDrinkState : DrinkState, IRoosterState
{
    private Rooster _rooster;

    public void Enter(Rooster rooster)
    {
        _rooster = rooster;
        _rooster.isDrinkState = true;
    }

    public override void Exit()
    {
        _rooster.thirstDuration = _rooster.animalStats.thirstDuration;
        _rooster.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _rooster.roosterAnimationState= Rooster.ROOSTER_ANIMATIONSTATE.ARM_COCK_EAT;

        if (_drinkTimer >= _drinkDuration)
        {
            _rooster.ChangeState(new RoosterPatrolState());
        }
    }
}
