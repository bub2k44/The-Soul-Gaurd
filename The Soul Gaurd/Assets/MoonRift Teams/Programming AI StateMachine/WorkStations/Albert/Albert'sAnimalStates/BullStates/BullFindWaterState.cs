using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullFindWaterState : FindWaterState, IBullState
{
    private Bull _bull;

    public void Enter(Bull bull)
    {
        _bull = bull;
        _bull.isFindWaterState = true;
        _bull._navMeshAgent.speed = _bull.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _bull.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _bull.FindWater();
        _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_WALK;

        if (_bull._navMeshAgent.remainingDistance <= 0f)
        {
            _bull.ChangeState(new BullDrinkState());
        }
    }
}
