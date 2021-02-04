using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakeDamageState : TakeDamageState, IBossState
{ 
    private Boss _boss;

    public void Enter(Boss boss)
    {
        _boss = boss;
        _boss.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _boss.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _boss.TakeDamage(5);
            _boss.bossAnimationState = Boss.BOSS_ANIMATIONSTATE.WOLF_HIT_FRONT;
            _tookDamage = true;
        }
        else
        {
            _boss.ChangeState(new BossIdleState());
        }
    }
}

