using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseChaseState : ChaseState ,IMooseState
{
    private Moose _moose;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose._navMeshAgent.speed = _moose.animalStats.chaseSpeed;
        _moose.isChaseState = true;
    }

    public override void Exit()
    {
        _moose.isChaseState = false;
    }

    protected override void Chase()
    {
        _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_RUN;
    }
}