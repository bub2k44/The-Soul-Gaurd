using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepPatrolState : PatrolState, ISheepState
{
    private Sheep _sheep;

    public void Enter(Sheep sheep)
    {
        _sheep = sheep;
        _sheep.isPatrolState = true;
        _sheep._navMeshAgent.speed = _sheep.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _sheep.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _sheep.Destination();
        _sheep.sheepAnimationState= Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_WALK;

        if (_patrolTimer >= _patrolDuration)
        {
            _sheep.ChangeState(new SheepIdleState());
        }
    }
}
