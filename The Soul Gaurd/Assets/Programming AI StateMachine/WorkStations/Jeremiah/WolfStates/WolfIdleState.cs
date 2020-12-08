using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfIdleState : IdleState, IWolfState
{
    private Wolf _wolf;

    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf.isIdleState = true;
        _wolf._navMeshAgent.speed = 0;///
    }

    public override void Exit()
    {
        _wolf.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        _wolf._navMeshAgent.speed = 0;

        if (_idleType == 1)
        {
            _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_IDLE;
        }
        else
        {
            _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_IDLE_LOOK;
        }

        if (_idleTimer >= _idleDuration)
        {
            _wolf.ChangeState(new WolfPatrolState());
        }
    }
}