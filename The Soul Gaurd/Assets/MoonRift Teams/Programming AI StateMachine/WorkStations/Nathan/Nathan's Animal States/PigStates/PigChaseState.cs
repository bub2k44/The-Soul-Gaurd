using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigChaseState : ChaseState ,IPigState
{
    private Pig _pig;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig._navMeshAgent.speed = _pig.animalStats.chaseSpeed;
        _pig.isChaseState = true;
    }

    public override void Exit()
    {
        _pig.isChaseState = false;
    }

    protected override void Chase()
    {
        _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_RUN;
    }
}