using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAwakenState : AwakenState, IBullState
{
    private Bull _bull;

    public void Enter(Bull bull)
    {
        _bull = bull;
        _bull.isAwakenState = true;
    }

    public override void Exit()
    {
        _bull.awakeDuration = _bull.animalStats.awakeDuration;
        _bull.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _bull.bullAnimationState = Bull.BULL_ANIMATIONSTATE.BULL_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _bull.ChangeState(new BullIdleState());
        }
    }
}
