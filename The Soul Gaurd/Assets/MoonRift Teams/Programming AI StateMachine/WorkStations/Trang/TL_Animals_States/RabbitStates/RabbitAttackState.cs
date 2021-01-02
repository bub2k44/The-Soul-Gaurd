using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAttackState : AttackState, IRabbitState
{
    private Rabbit _rabbit;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _rabbit._navMeshAgent.speed = 0;
        _rabbit.isAttackState = true;
    }

    public override void Exit()
    {
        _rabbit.isAttackState = false;
        _rabbit.attackFX.Stop();
        _rabbit._navMeshAgent.speed = _rabbit.animalStats.patrolSpeed;
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
            _rabbit.rabbitAnimationState = Rabbit.RABBIT_ANIMATIONSTATE.ARM_RABBIT_HIT_LEFT;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        //if (_rabbit.target.GetComponent<Animal>().currentHealth <= 0)
        //{
        //    _rabbit.ChangeState(new RabbitPatrolState());
        //}
    }
}
