using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamChaseState : ChaseState, IRamState
{
    private Ram _ram;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram._navMeshAgent.speed = _ram.animalStats.chaseSpeed;
        _ram.isChaseState = true;
    }

    public override void Exit()
    {
        _ram.isChaseState = false;
    }

    protected override void Chase()
    {
        _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_RUN;
    }
}