using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAttackState : AttackState, ISheepState
{
    private Sheep _sheep;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Sheep sheep)
    {
        _sheep = sheep;
        _sheep._navMeshAgent.speed = 0;
        _sheep.isAttackState = true;
    }

    public override void Exit()
    {
        _sheep.isAttackState = false;
        _sheep.attackFX.Stop();
        _sheep._navMeshAgent.speed = _sheep.animalStats.patrolSpeed;
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
            _sheep.sheepAnimationState= Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_ATTACK_1;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        if (_sheep.target.GetComponent<Animal>().currentHealth <= 0)
        {
            _sheep.ChangeState(new SheepPatrolState());
        }
    }
}
