using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterSleepState : SleepState, IRoosterState
{
    private Rooster _rooster;

    public void Enter(Rooster rooster)
    {
        _rooster= rooster;
        _rooster.sleepyFX.Play();
        _rooster.isSleepState = true;
        _rooster._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _rooster.sleepyFX.Stop();
        _rooster.isSleepState = false;
    }

    /*protected override void Sleeping()
    {
        base.Sleeping();
        _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _bear.ChangeState(new BearAwakenState());
        }
    }*/
}
