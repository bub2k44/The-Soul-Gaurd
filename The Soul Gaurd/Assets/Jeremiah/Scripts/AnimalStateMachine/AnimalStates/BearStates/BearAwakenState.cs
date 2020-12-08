using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAwakenState : AwakenState ,IBearState
{
    private Bear _bear;

    public void Enter(Bear bear)
    {
        _bear = bear;
        _bear.isAwakenState = true;
    }

    public  override void Exit()
    {
        _bear.awakeDuration = _bear.animalStats.awakeDuration;
        _bear.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _bear.bearAnimationState = Bear.BEAR_ANIMATIONSTATE.BEAR_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _bear.ChangeState(new BearIdleState());
        }
    }
}