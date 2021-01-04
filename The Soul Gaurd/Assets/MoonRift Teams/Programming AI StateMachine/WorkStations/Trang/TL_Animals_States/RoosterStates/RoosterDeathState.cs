using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterDeathState : DeathState, IRoosterState
{
    private Rooster _rooster;

    public void Enter(Rooster rooster)
    {
        _rooster = rooster;
        _rooster.isDeathState = true;
        _rooster._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _rooster.isDeathState = false;
        _rooster._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _rooster.roosterAnimationState = Rooster.ROOSTER_ANIMATIONSTATE.ARM_COCK_DEAD;
    }
}
