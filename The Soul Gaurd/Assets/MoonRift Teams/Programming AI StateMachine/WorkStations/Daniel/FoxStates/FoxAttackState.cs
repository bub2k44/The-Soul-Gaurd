using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAttackState : AttackState, IFoxState
{
    private Fox _fox;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox._navMeshAgent.speed = 0;
        _fox.isAttackState = true;
    }

    public override void Exit()
    {
        _fox.isAttackState = false;
        _fox.attackFX.Stop();
        _fox._navMeshAgent.speed = _fox.animalStats.patrolSpeed;
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
            _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_ATTACK;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        //if (_fox.target.GetComponent<Animal>().currentHealth <= 0)
        //{
        //    _fox.ChangeState(new FoxPatrolState());
        //}
    }
}
