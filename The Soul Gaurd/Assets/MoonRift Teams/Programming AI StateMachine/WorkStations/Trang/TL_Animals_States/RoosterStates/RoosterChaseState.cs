using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterChaseState : ChaseState, IRoosterState
{
    private Rooster _rooster;

    public void Enter(Rooster rooster)
    {
        _rooster = rooster;
        _rooster._navMeshAgent.speed = _rooster.animalStats.chaseSpeed;
        _rooster.isChaseState = true;
    }

    public override void Exit()
    {
        _rooster.isChaseState = false;
    }

    protected override void Chase()
    {
        _rooster.roosterAnimationState= Rooster.ROOSTER_ANIMATIONSTATE.ARM_COCK_RUN;
    }
}
