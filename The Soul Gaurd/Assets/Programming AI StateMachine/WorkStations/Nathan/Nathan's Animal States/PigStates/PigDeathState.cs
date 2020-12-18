using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigDeathState : DeathState, IPigState
{
    private Pig _pig;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig.isDeathState = true;
        _pig._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _pig.isDeathState = false;
        _pig._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {       
        _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_DIE;
    }
}