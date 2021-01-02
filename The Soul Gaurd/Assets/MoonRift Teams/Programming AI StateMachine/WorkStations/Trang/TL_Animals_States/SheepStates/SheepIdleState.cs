using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepIdleState : IdleState, ISheepState
{
    private Sheep _sheep;

    public void Enter(Sheep sheep)
    {
        _sheep = sheep;
        _sheep.isIdleState = true;
        _sheep._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _sheep.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _sheep.sheepAnimationState = Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_IDLE_1;
        }
        else
        {
            _sheep.sheepAnimationState = Sheep.SHEEP_ANIMATIONSTATE.ARM_SHEEP_IDLE_2;
        }

        if (_idleTimer >= _idleDuration)
        {
            _sheep.ChangeState(new SheepPatrolState());
        }
    }
}
