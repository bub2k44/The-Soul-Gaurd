using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeAttackState : AttackState, IDoeState
{
    private Doe _doe;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe._navMeshAgent.speed = 0;
        _doe.isAttackState = true;
    }

    public override void Exit()
    {
        _doe.isAttackState = false;
        _doe.attackFX.Stop();
        _doe._navMeshAgent.speed = _doe.animalStats.patrolSpeed;
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
            _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_ATTACK_HEADBUTT;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        if (_doe.target.GetComponent<Animal>().currentHealth <= 0)
        {
            _doe.ChangeState(new DoePatrolState());
        }
    }
}
