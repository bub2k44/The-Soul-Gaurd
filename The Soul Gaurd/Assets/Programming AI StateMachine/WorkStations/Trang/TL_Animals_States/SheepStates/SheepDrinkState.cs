using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepDrinkState : DrinkState, ISheepState
{
    private Sheep _sheep;

    public void Enter(Sheep sheep)
    {
        _sheep = sheep;
        _sheep.isDrinkState = true;
    }

    public override void Exit()
    {
        _sheep.thirstDuration = _sheep.animalStats.thirstDuration;
        _sheep.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _sheep.sheepAnimationState = Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_EAT_2;

        if (_drinkTimer >= _drinkDuration)
        {
            _sheep.ChangeState(new SheepPatrolState());
        }
    }
}
