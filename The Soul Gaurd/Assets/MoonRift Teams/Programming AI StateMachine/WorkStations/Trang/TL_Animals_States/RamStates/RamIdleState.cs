using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamIdleState : IdleState, IRamState
{
    private Ram _ram;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram.isIdleState = true;
        _ram._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _ram.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_IDLE_1;
        }
        else
        {
            _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_IDLE_2;
        }

        if (_idleTimer >= _idleDuration)
        {
            _ram.ChangeState(new RamPatrolState());
        }
    }
}