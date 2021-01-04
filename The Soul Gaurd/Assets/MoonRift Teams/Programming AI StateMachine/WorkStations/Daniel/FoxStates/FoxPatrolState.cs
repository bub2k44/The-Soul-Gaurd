using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxPatrolState : PatrolState, IFoxState
{
    private Fox _fox;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox.isPatrolState = true;
        _fox._navMeshAgent.speed = _fox.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _fox.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _fox.Destination();
        _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _fox.ChangeState(new FoxIdleState());
        }
    }
}
