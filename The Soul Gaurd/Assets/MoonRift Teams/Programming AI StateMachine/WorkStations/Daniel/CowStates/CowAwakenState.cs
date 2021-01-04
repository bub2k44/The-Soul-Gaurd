using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowAwakenState : AwakenState, ICowState
{
    private Cow _cow;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow.isAwakenState = true;
    }

    public override void Exit()
    {
        _cow.awakeDuration = _cow.animalStats.awakeDuration;
        _cow.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _cow.ChangeState(new CowIdleState());
        }
    }
}
