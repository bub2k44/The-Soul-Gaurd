using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAwakenState : AwakenState, IFoxState
{
    private Fox _fox;

    public void Enter(Fox fox)
    {
        _fox = fox;
        _fox.isAwakenState = true;
    }

    public override void Exit()
    {
        _fox.awakeDuration = _fox.animalStats.awakeDuration;
        _fox.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _fox.foxAnimationState = Fox.FOX_ANIMATIONSTATE.FOX_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _fox.ChangeState(new FoxIdleState());
        }
    }
}
