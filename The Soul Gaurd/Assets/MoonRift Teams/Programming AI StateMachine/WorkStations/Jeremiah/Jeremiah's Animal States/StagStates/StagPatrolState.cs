using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagPatrolState : PatrolState, IStagState
{
    private Stag _stag;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag.isPatrolState = true;
        _stag._navMeshAgent.speed = _stag.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _stag.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _stag.Destination();
        _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.WALK_1;

        if (_patrolTimer >= _patrolDuration)
        {
            _stag.ChangeState(new StagIdleState());
        }
    }
}
