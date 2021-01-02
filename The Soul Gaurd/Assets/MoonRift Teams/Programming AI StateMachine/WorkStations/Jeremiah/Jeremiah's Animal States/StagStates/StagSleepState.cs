using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagSleepState : SleepState, IStagState
{
    private Stag _stag;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag.sleepyFX.Play();
        _stag.isSleepState = true;
        _stag._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _stag.sleepyFX.Stop();
        _stag.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _stag.ChangeState(new StagAwakenState());
        }
    }
}
