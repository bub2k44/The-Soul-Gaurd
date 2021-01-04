using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxChaseState : ChaseState, IFoxState
{
    private Fox _fox;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox._navMeshAgent.speed = _fox.animalStats.chaseSpeed;
        _fox.isChaseState = true;
    }

    public override void Exit()
    {
        _fox.isChaseState = false;
    }

    protected override void Chase()
    {
        _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_RUN;
    }
}
