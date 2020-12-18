using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterAttackState : AttackState, IRoosterState
{
    private Rooster _rooster;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Rooster rooster)
    {
        _rooster= rooster;
        _rooster._navMeshAgent.speed = 0;
        _rooster.isAttackState = true;
    }

    public override void Exit()
    {
        _rooster.isAttackState = false;
        _rooster.attackFX.Stop();
        _rooster._navMeshAgent.speed = _rooster.animalStats.patrolSpeed;
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
            _rooster.roosterAnimationState = Rooster.ROOSTER_ANIMATIONSTATE.ARM_COCK_ATTACK;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        if (_rooster.target.GetComponent<Animal>().currentHealth <= 0)
        {
            _rooster.ChangeState(new RoosterPatrolState());
        }
    }
}
