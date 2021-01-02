using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSleepState : SleepState, IBearState
{
    private Bear _bear;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear.sleepyFX.Play();
        _bear.isSleepState = true;
        _bear._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _bear.sleepyFX.Stop();
        _bear.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _bear.ChangeState(new BearAwakenState());
        }
    }
}