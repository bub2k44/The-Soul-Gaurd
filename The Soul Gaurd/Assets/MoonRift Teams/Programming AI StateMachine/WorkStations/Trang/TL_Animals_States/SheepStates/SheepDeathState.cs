using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepDeathState :  DeathState, ISheepState
{
    private Sheep _sheep;

    public void Enter(Sheep sheep)
    {
        _sheep = sheep;
        _sheep.isDeathState = true;
        _sheep._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _sheep.isDeathState = false;
        _sheep._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _sheep.sheepAnimationState= Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_DEAD;
    }
}
