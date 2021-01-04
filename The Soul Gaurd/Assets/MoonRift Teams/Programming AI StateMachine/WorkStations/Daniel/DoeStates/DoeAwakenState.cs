using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeAwakenState : AwakenState, IDoeState
{
    private Doe _doe;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe.isAwakenState = true;
    }

    public override void Exit()
    {
        _doe.awakeDuration = _doe.animalStats.awakeDuration;
        _doe.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _doe.ChangeState(new DoeIdleState());
        }
    }
}
