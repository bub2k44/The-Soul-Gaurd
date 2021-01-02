using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxDeathState : DeathState, IFoxState
{
    private Fox _fox;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox.isDeathState = true;
        _fox._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _fox.isDeathState = false;
        _fox._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_DEATH;
    }
}
