using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSleepState : SleepState, IWolfState
{
    private Wolf _wolf;

    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf.sleepyFX.Play();
        _wolf.isSleepState = true;
        _wolf._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _wolf.sleepyFX.Stop();
        _wolf.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _wolf.ChangeState(new WolfAwakenState());
        }
    }
}