using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoePatrolState : PatrolState, IDoeState
{
    private Doe _doe;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe.isPatrolState = true;
        _doe._navMeshAgent.speed = _doe.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _doe.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _doe.Destination();
        _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _doe.ChangeState(new DoeIdleState());
        }
    }
}
