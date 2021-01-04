using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfDeathState : DeathState, ICalfState
{
    private Calf _calf;

    public void Enter(Calf calf)
    {
        _calf = calf;
        _calf.isDeathState = true;
        _calf._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _calf.isDeathState = false;
        _calf._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _calf.calfAnimationState = Calf.CALF_ANIMATIONSTATE.DIE;
    }
}
