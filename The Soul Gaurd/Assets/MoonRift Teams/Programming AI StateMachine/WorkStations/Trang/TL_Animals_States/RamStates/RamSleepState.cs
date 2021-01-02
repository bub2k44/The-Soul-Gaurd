using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamSleepState : SleepState, IRamState
{
    private Ram _ram;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram.sleepyFX.Play();
        _ram.isSleepState = true;
        _ram._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _ram.sleepyFX.Stop();
        _ram.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _ram.ChangeState(new RamAwakenState());
        }
    }
}
