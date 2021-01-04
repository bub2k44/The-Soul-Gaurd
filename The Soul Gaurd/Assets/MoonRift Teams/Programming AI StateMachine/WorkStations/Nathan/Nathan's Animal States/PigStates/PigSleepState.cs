using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSleepState : SleepState, IPigState
{
    private Pig _pig;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig.sleepyFX.Play();
        _pig.isSleepState = true;
        _pig._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _pig.sleepyFX.Stop();
        _pig.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _pig.ChangeState(new PigAwakenState());
        }
    }
}