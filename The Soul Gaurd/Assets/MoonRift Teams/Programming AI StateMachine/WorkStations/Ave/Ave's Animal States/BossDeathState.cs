using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : DeathState, IBossState
{
    private Boss _boss;

    private float timePassed;

    public void Enter(Boss boss)
    {
        _boss = boss;
        _boss.isDeathState = true;
        _boss._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _boss.isDeathState = false;
        _boss._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _boss.bossAnimationState = Boss.BOSS_ANIMATIONSTATE.WOLF_DEAD;
        timePassed += Time.deltaTime;

        if (timePassed >= 6)
        {
            _boss.GetComponent<NPC>().OnCharacterRemoved();
        }
    }
}
