using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSleepState : SleepState, IFoxState
{
    private Fox _fox;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox.sleepyFX.Play();
        _fox.isSleepState = true;
        _fox._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _fox.sleepyFX.Stop();
        _fox.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _fox.ChangeState(new FoxAwakenState());
        }
    }
}
