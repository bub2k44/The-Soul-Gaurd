using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepChaseState : ChaseState, ISheepState
{
    private Sheep _sheep;

    public void Enter(Sheep sheep)
    {
        _sheep= sheep;
        _sheep._navMeshAgent.speed = _sheep.animalStats.chaseSpeed;
        _sheep.isChaseState = true;
    }

    public override void Exit()
    {
        _sheep.isChaseState = false;
    }

    protected override void Chase()
    {
        _sheep.sheepAnimationState = Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_RUN;
    }
}
