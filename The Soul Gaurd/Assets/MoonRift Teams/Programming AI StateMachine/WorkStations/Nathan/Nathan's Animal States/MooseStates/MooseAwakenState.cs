using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseAwakenState : AwakenState ,IMooseState
{
    private Moose _moose;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose.isAwakenState = true;
    }

    public  override void Exit()
    {
        _moose.awakeDuration = _moose.animalStats.awakeDuration;
        _moose.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _moose.ChangeState(new MooseIdleState());
        }
    }
}