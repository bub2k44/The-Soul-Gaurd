using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowAttackState : AttackState, ICowState
{
    private Cow _cow;

    public float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow._navMeshAgent.speed = 0;
        _cow.isAttackState = true;
    }

    public override void Exit()
    {
        _cow.isAttackState = false;
        _cow.attackFX.Stop();
        _cow._navMeshAgent.speed = _cow.animalStats.patrolSpeed;
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
            _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_ATTACK_HEADBUTT;
            _attackTimer = 0f;
            attacking = false;
        }
        else
        {
            attacking = false;
        }

        //if (_cow.target.GetComponent<Animal>().currentHealth <= 0)
        //{
        //    _cow.ChangeState(new CowPatrolState());
        //}
    }
}
