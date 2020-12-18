using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamPatrolState : PatrolState, IRamState
{
    private Ram _ram;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram.isPatrolState = true;
        _ram._navMeshAgent.speed = _ram.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _ram.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _ram.Destination();
        _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _ram.ChangeState(new RamIdleState());
        }
    }
}