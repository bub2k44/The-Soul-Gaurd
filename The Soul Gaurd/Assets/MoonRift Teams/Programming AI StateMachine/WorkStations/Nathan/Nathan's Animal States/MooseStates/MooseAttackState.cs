using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseAttackState : AttackState, IMooseState
{
    private Moose _moose;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose._navMeshAgent.speed = 0;
        _moose.isAttackState = true;
    }

    public override void Exit()
    {
        _moose.isAttackState = false;
        _moose.attackFX.Stop();
        _moose._navMeshAgent.speed = _moose.animalStats.patrolSpeed;
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
            _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_ATTACK_1;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        //if (_moose.target.GetComponent<Animal>().currentHealth <= 0)
        //{
        //    _moose.ChangeState(new MoosePatrolState());
        //}
    }
}