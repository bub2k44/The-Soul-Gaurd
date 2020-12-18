using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowFindWaterState : FindWaterState, ICowState
{
    private Cow _cow;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow.isFindWaterState = true;
        _cow._navMeshAgent.speed = _cow.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _cow.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _cow.FindWater();
        _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_WALK;

        if (_cow._navMeshAgent.remainingDistance <= 0f)
        {
            _cow.ChangeState(new CowDrinkState());
        }
    }
}
