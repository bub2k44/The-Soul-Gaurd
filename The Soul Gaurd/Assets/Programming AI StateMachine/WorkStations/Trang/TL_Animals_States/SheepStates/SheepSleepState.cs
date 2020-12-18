using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSleepState : SleepState, ISheepState
{
    private Sheep _sheep;

    public void Enter(Sheep sheep)
    {
        _sheep = sheep;
        _sheep.sleepyFX.Play();
        _sheep.isSleepState = true;
        _sheep._navMeshAgent.speed = 0;
    }

    public override void Exit()
    {
        _sheep.sleepyFX.Stop();
        _sheep.isSleepState = false;
    }

    protected override void Sleeping()
    {
        base.Sleeping();
        _sheep.sheepAnimationState = Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_SLEEP_START;

        if (_sleepTimer >= _sleepDuration)
        {
            _sheep.ChangeState(new SheepAwakenState());
        }
    }
}
