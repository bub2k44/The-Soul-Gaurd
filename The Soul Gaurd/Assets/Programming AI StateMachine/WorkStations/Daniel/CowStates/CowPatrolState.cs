using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowPatrolState : PatrolState, ICowState
{
    private Cow _cow;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow.isPatrolState = true;
        _cow._navMeshAgent.speed = _cow.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _cow.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _cow.Destination();
        _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _cow.ChangeState(new CowIdleState());
        }
    }
}
