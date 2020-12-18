using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamAwakenState : AwakenState, IRamState
{
    private Ram _ram;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram.isAwakenState = true;
    }

    public override void Exit()
    {
        _ram.awakeDuration = _ram.animalStats.awakeDuration;
        _ram.isAwakenState = false;
    }

    protected override void Awaken()
    {
        base.Awaken();
        _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_SLEEP_END;

        if (_wakeUpTimer >= _wakeUpDuration)
        {
            _ram.ChangeState(new RamIdleState());
        }
    }
}
