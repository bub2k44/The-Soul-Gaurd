using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepFindWaterState : FindWaterState, ISheepState
{
    private Sheep _sheep;

    public void Enter(Sheep sheep)
    {
        _sheep = sheep;
        _sheep.isFindWaterState = true;
        _sheep._navMeshAgent.speed = _sheep.animalStats.patrolSpeed;
    }

    public override void Exit()
    {
        _sheep.isFindWaterState = false;
    }

    protected override void FindWater()
    {
        _sheep.FindWater();
        _sheep.sheepAnimationState= Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_WALK;

        if (_sheep._navMeshAgent.remainingDistance <= 0f)
        {
            _sheep.ChangeState(new SheepDrinkState());
        }
    }
}
