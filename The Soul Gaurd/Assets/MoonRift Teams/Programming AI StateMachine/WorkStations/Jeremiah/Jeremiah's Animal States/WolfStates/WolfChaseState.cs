using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfChaseState : ChaseState, IWolfState
{
    private Wolf _wolf;

    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf._navMeshAgent.speed = _wolf.animalStats.chaseSpeed;
        _wolf.isChaseState = true;
    }

    public override void Exit()
    {
        _wolf.isChaseState = false;
    }

    protected override void Chase()
    {
        _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_RUN;
    }
}