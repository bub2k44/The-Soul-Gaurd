using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagAttackState : AttackState, IStagState
{
    private Stag _stag;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag._navMeshAgent.speed = 0;
        _stag.isAttackState = true;
    }

    public override void Exit()
    {
        _stag.isAttackState = false;
        _stag.attackFX.Stop();
        _stag._navMeshAgent.speed = _stag.animalStats.patrolSpeed;
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
            _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.ATTACK_1;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        //if (_stag.target.GetComponent<Animal>().currentHealth <= 0)
        //{
        //    _stag.ChangeState(new StagPatrolState());
        //}
    }
}
