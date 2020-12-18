using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAttackState : AttackState, IBullState
{
    private Bull _bull;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Bull bull)
    {
        _bull = bull;
        _bull._navMeshAgent.speed = 0;
        _bull.isAttackState = true;
    }

    public override void Exit()
    {
        _bull.isAttackState = false;
        _bull.attackFX.Stop();
        _bull._navMeshAgent.speed = _bull.animalStats.patrolSpeed;
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
            _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_ATTACK;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        if (_bull.target.GetComponent<Animal>().currentHealth <= 0)
        {
            _bull.ChangeState(new BullPatrolState());
        }
    }
}
