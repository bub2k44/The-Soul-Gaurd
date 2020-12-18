using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamDrinkState : DrinkState, IRamState
{
    private Ram _ram;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram.isDrinkState = true;
    }

    public override void Exit()
    {
        _ram.thirstDuration = _ram.animalStats.thirstDuration;
        _ram.isDrinkState = false;
    }

    protected override void Eat()
    {
        base.Eat();
        _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_EAT_1;

        if (_drinkTimer >= _drinkDuration)
        {
            _ram.ChangeState(new RamPatrolState());
        }
    }
}