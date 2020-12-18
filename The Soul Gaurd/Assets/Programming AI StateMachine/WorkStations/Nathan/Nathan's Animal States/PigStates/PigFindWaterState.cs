using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigFindWaterState : FindWaterState, IPigState
{
    private Pig _pig;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig.isFindWaterState = true;
        _pig._navMeshAgent.speed = _pig.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _pig.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _pig.FindWater();
        _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_WALK;

        if (_pig._navMeshAgent.remainingDistance <= 0f)
        {
            _pig.ChangeState(new PigDrinkState());
        }
    }
}