using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : AttackState, IBossState
{
    private Boss _boss;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Boss boss)
    {
        _boss = boss;
        _boss._navMeshAgent.speed = 0;
        _boss.isAttackState = true;
    }

    public override void Exit()
    {
        _boss.isAttackState = false;
        _boss.attackFX.Stop();
        _boss._navMeshAgent.speed = _boss.animalStats.patrolSpeed;
    }

    protected override void Attack()
    {
        if (!attacking)
        {
            _attackTimer += Time.deltaTime;
        }

        if (_attackTimer >= _attackDuration)
        {
            attacking = true;
            _boss.bossAnimationState = Boss.BOSS_ANIMATIONSTATE.WOLF_ATTACK;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        //if (_bear.target.GetComponent<Animal>().currentHealth <= 0)
        //{
        //    _bear.ChangeState(new BearPatrolState());
        //}
    }
}
