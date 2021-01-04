using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamFindWaterState : FindWaterState, IRamState
{
    private Ram _ram;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram.isFindWaterState = true;
        _ram._navMeshAgent.speed = _ram.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _ram.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _ram.FindWater();
        _ram.ramAnimationState= Ram.RAM_ANIMATIONSTATE.ARM_RAM_WALK;

        if (_ram._navMeshAgent.remainingDistance <= 0f)
        {
            _ram.ChangeState(new RamDrinkState());
        }
    }
}