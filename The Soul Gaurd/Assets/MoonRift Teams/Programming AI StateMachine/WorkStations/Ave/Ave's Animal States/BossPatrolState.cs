using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrolState : PatrolState, IBossState
{
    private Boss _boss;

    public void Enter(Boss boss)
    {
        _boss= boss;
        _boss.isPatrolState = true;
        _boss._navMeshAgent.speed = _boss.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _boss.isPatrolState = false;
    }

    protected override void Patrol()
    {
        base.Patrol();
        _boss.Destination();
        _boss.bossAnimationState= Boss.BOSS_ANIMATION_STATE.WOLF_WALK_REGULAR;

        if (_patrolTimer >= _patrolDuration)
        {
            _boss.ChangeState(new BossIdleState());
        }
    }
}