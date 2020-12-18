using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseSleepState : SleepState, IMooseState
{
    private Moose _moose;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose.sleepyFX.Play();
        _moose.isSleepState = true;
        _moose._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _moose.sleepyFX.Stop();
        _moose.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _moose.ChangeState(new MooseAwakenState());
        }
    }
}