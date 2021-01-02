using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowAlertState : AlertState, ICowState
{
    private Cow _cow;
    private SphereCollider cowCollider;

    public void Enter(Cow cow)
    {
        _cow = cow;
        _cow.isAlertState = true;
        _cow._navMeshAgent.speed = 0;//
        cowCollider = _cow.gameObject.GetComponent<SphereCollider>();
        cowCollider.radius += 5;
    }

    public override void Exit()
    {
        cowCollider.radius -= 5;
        _cow.isAlertState = false;
    }

    protected override void Alert()
    {
        base.Alert();

        _cow.cowAnimationState = Cow.COW_ANIMATIONSTATE.COW_IDLE;

        if (_alertTimer >= _alertDuration)
        {
            _cow.ChangeState(new CowPatrolState());
        }
    }
}
