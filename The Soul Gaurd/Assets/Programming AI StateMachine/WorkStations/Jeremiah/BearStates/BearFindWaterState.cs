using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearFindWaterState : FindWaterState, IBearState
{
    private Bear _bear;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear.isFindWaterState = true;
        _bear._navMeshAgent.speed = _bear.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _bear.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _bear.FindWater();
        _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_WALK;

        if (_bear._navMeshAgent.remainingDistance <= 0f)
        {
            _bear.ChangeState(new BearDrinkState());
        }
    }
}