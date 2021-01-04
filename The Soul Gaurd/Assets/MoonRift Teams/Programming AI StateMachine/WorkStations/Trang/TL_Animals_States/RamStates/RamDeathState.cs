using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamDeathState : DeathState, IRamState
{
    private Ram _ram;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram.isDeathState = true;
        _ram._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _ram.isDeathState = false;
        _ram._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_DEAD;
    }
}
