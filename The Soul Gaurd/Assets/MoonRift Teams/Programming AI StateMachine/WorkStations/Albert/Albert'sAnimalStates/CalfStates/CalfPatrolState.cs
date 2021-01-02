using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfPatrolState : PatrolState, ICalfState
{
    private Calf _calf;

    public void Enter(Calf calf)
    {
        _calf = calf;
        _calf.isPatrolState = true;
        _calf._navMeshAgent.speed = _calf.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _calf.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _calf.Destination();
        _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.WALK_1;

        if (_patrolTimer >= _patrolDuration)
        {
            _calf.ChangeState(new CalfIdleState());
        }
    }
}
