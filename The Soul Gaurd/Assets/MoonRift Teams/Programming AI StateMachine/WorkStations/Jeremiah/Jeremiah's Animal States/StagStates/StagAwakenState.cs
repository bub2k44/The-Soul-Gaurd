using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagAwakenState : AwakenState, IStagState
{
    private Stag _stag;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag.isAwakenState = true;
    }

    public override void Exit()
    {
        _stag.awakeDuration = _stag.animalStats.awakeDuration;
        _stag.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();

        _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _stag.ChangeState(new StagIdleState());
        }
    }
}
