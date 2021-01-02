using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseDeathState : DeathState, IMooseState
{
    private Moose _moose;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose.isDeathState = true;
        _moose._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _moose.isDeathState = false;
        _moose._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {       
        _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_DIE;
    }
}