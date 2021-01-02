using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttackState : AttackState, IWolfState
{
    private Wolf _wolf;

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
        _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_ATTACK;

        //if (_wolf.target.GetComponent<Animal>().currentHealth <= 0)
        //{
        //    _wolf.ChangeState(new WolfPatrolState());
        //}
    }
}