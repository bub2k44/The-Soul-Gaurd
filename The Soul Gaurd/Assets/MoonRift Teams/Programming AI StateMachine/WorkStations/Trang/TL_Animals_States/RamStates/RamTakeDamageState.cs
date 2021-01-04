using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamTakeDamageState : TakeDamageState, IRamState
{
    private Ram _ram;

    public void Enter(Ram ram)
    {
        _ram = ram;
        _ram.isTakeDamageState = true;
    }

    public override void Exit()
    {
        _ram.isTakeDamageState = false;
    }

    protected override void TakeDamage()
    {
        if (!_tookDamage)
        {
            _ram.TakeDamage(5);
            _ram.ramAnimationState = Ram.RAM_ANIMATIONSTATE.ARM_RAM_HIT_FRONT;
            _tookDamage = true;
        }
        else
        {
            _ram.ChangeState(new RamIdleState());
        }
    }
}
