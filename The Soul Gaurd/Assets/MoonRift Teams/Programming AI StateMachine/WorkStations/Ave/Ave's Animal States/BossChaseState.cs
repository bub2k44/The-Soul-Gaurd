using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : ChaseState, IBossState
{
    private Boss _boss;

    public void Enter(Boss boss)
    {
        _boss= boss;
        _boss._navMeshAgent.speed = _boss.animalStats.chaseSpeed;
        _boss.isChaseState = true;
    }

    public override void Exit()
    {
        _boss.isChaseState = false;
    }

    protected override void Chase()
    {
        _boss.bossAnimationState = Boss.BOSS_ANIMATION_STATE.WOLF_RUN;
    }
}