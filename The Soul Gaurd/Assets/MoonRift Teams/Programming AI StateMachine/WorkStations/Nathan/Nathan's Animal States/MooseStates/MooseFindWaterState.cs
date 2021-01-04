using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseFindWaterState : FindWaterState, IMooseState
{
    private Moose _moose;

    public void Enter(Moose moose)
    {
        _moose = moose;
        _moose.isFindWaterState = true;
        _moose._navMeshAgent.speed = _moose.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _moose.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _moose.FindWater();
        _moose.mooseAnimationState = Moose.MOOSE_ANIMATIONSTATE.MOOSE_WALK_1;

        if (_moose._navMeshAgent.remainingDistance <= 0f)
        {
            _moose.ChangeState(new MooseDrinkState());
        }
    }
}