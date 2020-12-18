using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterPatrolState : PatrolState, IRoosterState
{
    private Rooster _rooster;

    public void Enter(Rooster rooster)
    {
        _rooster= rooster;
        _rooster.isPatrolState = true;
        _rooster._navMeshAgent.speed = _rooster.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _rooster.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _rooster.Destination();
        _rooster.roosterAnimationState= Rooster.ROOSTER_ANIMATIONSTATE.ARM_COCK_WALK_SLOW;

        if (_patrolTimer >= _patrolDuration)
        {
            _rooster.ChangeState(new RoosterIdleState());
        }
    }
}
