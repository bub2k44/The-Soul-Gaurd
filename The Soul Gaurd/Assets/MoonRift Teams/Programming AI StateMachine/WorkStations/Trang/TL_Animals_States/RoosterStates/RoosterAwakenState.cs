using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterAwakenState : AwakenState, IRoosterState
{
    private Rooster _rooster;

    public void Enter(Rooster rooster)
    {
        _rooster = rooster;
        _rooster.isAwakenState = true;
    }

    public override void Exit()
    {
        _rooster.awakeDuration = _rooster.animalStats.awakeDuration;
        _rooster.isAwakenState = false;
    }

    protected override void Awaken()
    {
        /*base.Awaken();
        _bear.bearAnimationState = Rooster.ROOSTER_ANIMATIONSTATE.BEAR_SLEEP_END;*/

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _rooster.ChangeState(new RoosterIdleState());
        }
    }
}
