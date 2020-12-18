using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAwakenState : AwakenState, ISheepState
{
    private Sheep _sheep;

    public void Enter(Sheep sheep)
    {
        _sheep = sheep;
        _sheep.isAwakenState = true;
    }

    public override void Exit()
    {
        _sheep.awakeDuration = _sheep.animalStats.awakeDuration;
        _sheep.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _sheep.sheepAnimationState= Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _sheep.ChangeState(new SheepIdleState());
        }
    }
}
