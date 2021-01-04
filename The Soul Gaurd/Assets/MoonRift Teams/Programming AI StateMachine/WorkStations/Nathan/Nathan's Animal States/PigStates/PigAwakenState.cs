using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAwakenState : AwakenState ,IPigState
{
    private Pig _pig;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig.isAwakenState = true;
    }

    public  override void Exit()
    {
        _pig.awakeDuration = _pig.animalStats.awakeDuration;
        _pig.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _pig.ChangeState(new PigIdleState());
        }
    }
}