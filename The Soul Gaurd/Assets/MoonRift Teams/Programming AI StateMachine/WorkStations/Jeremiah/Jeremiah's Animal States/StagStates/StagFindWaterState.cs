using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagFindWaterState : FindWaterState, IStagState
{
    private Stag _stag;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag.isFindWaterState = true;
        _stag._navMeshAgent.speed = _stag.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _stag.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _stag.FindWater();
        _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.WALK_1;

        if (_stag._navMeshAgent.remainingDistance <= 0f)
        {
            _stag.ChangeState(new StagDrinkState());
        }
    }
}
