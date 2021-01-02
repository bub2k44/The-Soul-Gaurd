using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoosePatrolState : PatrolState, IMooseState
{
    private Moose _moose;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose.isPatrolState = true;
        _moose._navMeshAgent.speed = _moose.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _moose.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _moose.Destination();
        _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_WALK_1;

        if (_patrolTimer >= _patrolDuration)
        {
            _moose.ChangeState(new MooseIdleState());
        }
    }
}