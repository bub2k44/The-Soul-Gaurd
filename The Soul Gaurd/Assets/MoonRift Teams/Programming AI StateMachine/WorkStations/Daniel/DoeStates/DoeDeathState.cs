using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeDeathState : DeathState, IDoeState
{
    private Doe _doe;

    public void Enter(Doe doe)
    {
        _doe = doe;
        _doe.isDeathState = true;
        _doe._navMeshAgent.enabled = false;
    }

    public override void Exit()
    {
        _doe.isDeathState = false;
        _doe._navMeshAgent.enabled = true;
    }

    protected override void Death()
    {
        _doe.doeAnimationState = Doe.DOE_ANIMATIONSTATE.DOE_DEATH;
    }
}
