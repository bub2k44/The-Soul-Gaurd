using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfFindWaterState : FindWaterState, ICalfState
{
    private Calf _calf;

    public void Enter(Calf calf)
    {
        _calf = calf;
        _calf.isFindWaterState = true;
        _calf._navMeshAgent.speed = _calf.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _calf.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _calf.FindWater();
        _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.WALK_1;

        if (_calf._navMeshAgent.remainingDistance <= 0f)
        {
            _calf.ChangeState(new CalfDrinkState());
        }
    }
}
