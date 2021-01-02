using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfAwakenState : AwakenState, ICalfState
{
    private Calf _calf;

    public void Enter(Calf calf)
    {
        _calf = calf;
        _calf.isAwakenState = true;
    }

    public override void Exit()
    {
        _calf.awakeDuration = _calf.animalStats.awakeDuration;
        _calf.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _calf.ChangeState(new CalfIdleState());
        }
    }
}
