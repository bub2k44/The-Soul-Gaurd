using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeSleepState : SleepState, IDoeState
{
    private Doe _doe;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe.sleepyFX.Play();
        _doe.isSleepState = true;
        _doe._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _doe.sleepyFX.Stop();
        _doe.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _doe.ChangeState(new DoeAwakenState());
        }
    }
}
