using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFindWaterState : FindWaterState, IWolfState
{
    private Wolf _wolf;

    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf.isFindWaterState = true;
        _wolf._navMeshAgent.speed = _wolf.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _wolf.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _wolf.FindWater();
        _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_WALK_REGULAR;

        if (_wolf._navMeshAgent.remainingDistance <= 0f)
        {
            _wolf.ChangeState(new WolfDrinkState());
        }
    }
}