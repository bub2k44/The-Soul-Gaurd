using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttackState : AttackState, IWolfState
{
    private Wolf _wolf;
    private float _attackTimer = 4f;
    private float _attackDuration = 10f;
    private bool attacking = false;


    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf._navMeshAgent.speed = 0;
        _wolf.isAttackState = true;
    }

    public override void Exit()
    {
        _wolf.isAttackState = false;
        _wolf.attackFX.Stop();
        _wolf._navMeshAgent.speed = _wolf.animalStats.patrolSpeed;
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
            _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_ATTACK;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        //if (_wolf.target.GetComponent<Animal>().currentHealth <= 0)
        //{
        //    _wolf.ChangeState(new WolfPatrolState());
        //}
    }
}