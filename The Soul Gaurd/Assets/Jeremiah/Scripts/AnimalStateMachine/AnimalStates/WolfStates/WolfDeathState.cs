using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDeathState : DeathState, IWolfState
{
    private Wolf _wolf;

    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf.isDeathState = true;
        _wolf._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _wolf.isDeathState = false;
        _wolf._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_DEAD;
    }
}