using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAwakenState : AwakenState, IWolfState
{
    private Wolf _wolf;

    public void Enter(Wolf wolf)
    {
        _wolf = wolf;
        _wolf.isAwakenState = true;
    }

    public override void Exit()
    {
        _wolf.awakeDuration = _wolf.animalStats.awakeDuration;
        _wolf.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _wolf.wolfAnimationState = Wolf.WOLF_ANIMATION_STATE.WOLF_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _wolf.ChangeState(new WolfIdleState());
        }
    }
}