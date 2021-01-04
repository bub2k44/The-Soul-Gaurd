using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterFindWaterState : FindWaterState, IRoosterState
{
    private Rooster _rooster;

    public void Enter(Rooster rooster)
    {
        _rooster = rooster;
        _rooster.isFindWaterState = true;
        _rooster._navMeshAgent.speed = _rooster.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _rooster.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _rooster.FindWater();
        _rooster.roosterAnimationState= Rooster.ROOSTER_ANIMATIONSTATE.ARM_COCK_WALK_SEARCH;

        if (_rooster._navMeshAgent.remainingDistance <= 0f)
        {
            _rooster.ChangeState(new RoosterDrinkState());
        }
    }
}
