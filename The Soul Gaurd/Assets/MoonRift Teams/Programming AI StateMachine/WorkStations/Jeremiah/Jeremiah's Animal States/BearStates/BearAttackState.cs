using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttackState : AttackState, IBearState
{
    private Bear _bear;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear._navMeshAgent.speed = 0;
        _bear.isAttackState = true;
    }

    public override void Exit()
    {
        _bear.isAttackState = false;
        _bear.attackFX.Stop();
        _bear._navMeshAgent.speed = _bear.animalStats.patrolSpeed;
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
            _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_ATTACK_SWIPE;
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