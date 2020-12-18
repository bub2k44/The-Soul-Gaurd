using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigPatrolState : PatrolState, IPigState
{
    private Pig _pig;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig.isPatrolState = true;
        _pig._navMeshAgent.speed = _pig.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _pig.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _pig.Destination();
        _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _pig.ChangeState(new PigIdleState());
        }
    }
}