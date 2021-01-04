using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfAttackState : AttackState, ICalfState
{
    private Calf _calf;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Calf calf)
    {
        _calf = calf;
        _calf._navMeshAgent.speed = 0;
        _calf.isAttackState = true;
    }

    public override void Exit()
    {
        _calf.isAttackState = false;
        _calf.attackFX.Stop();
        _calf._navMeshAgent.speed = _calf.animalStats.patrolSpeed;
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
            _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.ATTACK_1;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        if (_calf.target.GetComponent<Animal>().currentHealth <= 0)
        {
            _calf.ChangeState(new CalfPatrolState());
        }
    }
}
