using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeFindWaterState : FindWaterState, IDoeState
{
    private Doe _doe;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe.isFindWaterState = true;
        _doe._navMeshAgent.speed = _doe.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _doe.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _doe.FindWater();
        _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_WALK;

        if (_doe._navMeshAgent.remainingDistance <= 0f)
        {
            _doe.ChangeState(new DoeDrinkState());
        }
    }
}
