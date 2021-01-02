using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowFleeState : FleeState, ICowState
{
    private Cow _cow;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow._navMeshAgent.speed = _cow.animalStats.chaseSpeed;
        _cow.isFleeState = true;
        _cow.FleeTarget(_cow.target.transform);
    }

    public override void Exit()
    {
        _cow.isFleeState = false;
    }

    protected override void Flee()
    {
        _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_RUN;

        if ((_cow.transform.position - _cow.target.transform.position).magnitude >= 10)
        {
            _cow.target = null;
            _cow.ChangeState(new CowAlertState());
        }
    }
}
