using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullPatrolState : PatrolState, IBullState
{
    private Bull _bull;

    public void Enter(Bull bull)
    {
        _bull = bull;
        _bull.isPatrolState = true;
        _bull._navMeshAgent.speed = _bull.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _bull.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _bull.Destination();
        _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _bull.ChangeState(new BullIdleState());
        }
    }
}
