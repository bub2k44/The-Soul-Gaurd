using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAttackState : AttackState, IDogState
{
    private Dog _dog;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Dog dog)
    {
        _dog = dog;
        _dog._navMeshAgent.speed = 0;
        _dog.isAttackState = true;
    }

    public override void Exit()
    {
        _dog.isAttackState = false;
        _dog.attackFX.Stop();
        _dog._navMeshAgent.speed = _dog.animalStats.patrolSpeed;
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
            _dog.dogAnimationState = Dog.DOG_ANIMATIONSTATE.DOG_ATTACK;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        if (_dog.target.GetComponent<Animal>().currentHealth <= 0)
        {
            _dog.ChangeState(new DogPatrolState());
        }
    }
}
