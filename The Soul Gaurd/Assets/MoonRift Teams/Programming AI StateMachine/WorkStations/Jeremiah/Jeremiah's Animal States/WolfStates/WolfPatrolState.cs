using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPatrolState : PatrolState, IWolfState
{
    private Wolf _wolf;

    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf.isPatrolState = true;
        _wolf._navMeshAgent.speed = _wolf.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _wolf.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _wolf.Destination();
        _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_WALK_REGULAR;

        if (_patrolTimer >= _patrolDuration)
        {
            _wolf.ChangeState(new WolfIdleState());
        }
    }
}