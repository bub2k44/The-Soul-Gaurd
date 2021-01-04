using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagDeathState : DeathState, IStagState
{
    private Stag _stag;

    public void Enter(Stag stag)
    {
        _stag = stag;
        _stag.isDeathState = true;
        _stag._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _stag.isDeathState = false;
        _stag._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _stag.stagAnimationState = Stag.STAG_ANIMATIONSTATE.DIE;
    }
}
