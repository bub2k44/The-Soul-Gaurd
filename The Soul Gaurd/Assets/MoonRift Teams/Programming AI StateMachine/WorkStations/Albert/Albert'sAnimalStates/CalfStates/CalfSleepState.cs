using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfSleepState : SleepState, ICalfState
{
    private Calf _calf;

    public void Enter(Calf calf)
    {
        _calf = calf;
        _calf.sleepyFX.Play();
        _calf.isSleepState = true;
        _calf._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _calf.sleepyFX.Stop();
        _calf.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _calf.ChangeState(new CalfAwakenState());
        }
    }
}
