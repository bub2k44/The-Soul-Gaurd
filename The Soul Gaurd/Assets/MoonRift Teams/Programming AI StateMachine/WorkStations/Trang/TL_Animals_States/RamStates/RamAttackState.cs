using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamAttackState : AttackState, IRamState
{
    private Ram _ram;

    private float _attackTimer = 4f;
    private float _attackDuration = 4f;
    private bool attacking = false;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram._navMeshAgent.speed = 0;
        _ram.isAttackState = true;
    }

    public override void Exit()
    {
        _ram.isAttackState = false;
        _ram.attackFX.Stop();
        _ram._navMeshAgent.speed = _ram.animalStats.patrolSpeed;
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
            _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_ATTACK_1;
            _attackTimer = 0f;
        }
        else
        {
            attacking = false;
        }

        if (_ram.target.GetComponent<Animal>().currentHealth <= 0)
        {
            _ram.ChangeState(new RamPatrolState());
        }
    }
}
