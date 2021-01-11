using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAttackState : AttackState, IPigState
{
    private Pig _pig;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig._navMeshAgent.speed = 0;
        _pig.isAttackState = true;
    }

    public override void Exit()
    {
        _pig.isAttackState = false;
        _pig.attackFX.Stop();
        _pig._navMeshAgent.speed = _pig.animalStats.patrolSpeed;
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
            _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_ATTACK;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        //if (_pig.target.GetComponent<Animal>().currentHealth <= 0)
        //{
        //    _pig.ChangeState(new PigPatrolState());
        //}
    }
}