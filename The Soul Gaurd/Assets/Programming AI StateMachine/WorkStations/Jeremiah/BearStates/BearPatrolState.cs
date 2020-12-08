using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearPatrolState : PatrolState, IBearState
{
    private Bear _bear;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear.isPatrolState = true;
        _bear._navMeshAgent.speed = _bear.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _bear.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _bear.Destination();
        _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _bear.ChangeState(new BearIdleState());
        }
    }
}