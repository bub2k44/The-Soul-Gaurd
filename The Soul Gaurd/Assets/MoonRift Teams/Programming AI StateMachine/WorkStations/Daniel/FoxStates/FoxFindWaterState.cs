using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxFindWaterState : FindWaterState, IFoxState
{
    private Fox _fox;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox.isFindWaterState = true;
        _fox._navMeshAgent.speed = _fox.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _fox.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _fox.FindWater();
        _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_WALK;

        if (_fox._navMeshAgent.remainingDistance <= 0f)
        {
            _fox.ChangeState(new FoxDrinkState());
        }
    }
}
