using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSleepState : SleepState, ICowState
{
    private Cow _cow;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow.sleepyFX.Play();
        _cow.isSleepState = true;
        _cow._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _cow.sleepyFX.Stop();
        _cow.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _cow.ChangeState(new CowAwakenState());
        }
    }
}
