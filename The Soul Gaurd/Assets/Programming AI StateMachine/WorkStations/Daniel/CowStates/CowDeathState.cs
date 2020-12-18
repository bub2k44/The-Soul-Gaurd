using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowDeathState : DeathState, ICowState
{
    private Cow _cow;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow.isDeathState = true;
        _cow._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _cow.isDeathState = false;
        _cow._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_DEATH;
    }
}
