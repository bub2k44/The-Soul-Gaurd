using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSleepState : SleepState, IBullState
{
    private Bull _bull;

    public void Enter(Bull bull)
    {
        _bull = bull;
        _bull.sleepyFX.Play();
        _bull.isSleepState = true;
        _bull._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _bull.sleepyFX.Stop();
        _bull.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _bull.ChangeState(new BullAwakenState());
        }
    }
}
