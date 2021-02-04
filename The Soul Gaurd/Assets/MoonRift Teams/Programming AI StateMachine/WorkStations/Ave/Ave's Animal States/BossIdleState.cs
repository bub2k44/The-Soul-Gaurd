using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : IdleState, IBossState
{
    private Boss _boss;

    public void Enter(Boss boss)
    {
        _boss = boss;
        _boss.isIdleState = true;
        _boss._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _boss.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _boss.bossAnimationState = Boss.BOSS_ANIMATIONSTATE.WOLF_IDLE;
        }
        else
        {
            _boss.bossAnimationState = Boss.BOSS_ANIMATIONSTATE.WOLF_IDLE_LOOK;
        }

        //if (_idleTimer >= _idleDuration)
        //{
        //    _boss.ChangeState(new BossPatrolState());
        //}
    }
}
