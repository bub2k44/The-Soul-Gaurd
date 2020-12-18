using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigIdleState : IdleState, IPigState
{
    private Pig _pig;

    public void Enter(Pig pig)
    {
        _pig = pig;
        _pig.isIdleState = true;
        _pig._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _pig.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_IDLE_1;
        }
        else
        {
            _pig.pigAnimationState = Pig.PIG_ANIMATIONSTATE.PIG_IDLE_2;
        }

        if (_idleTimer >= _idleDuration)
        {
            _pig.ChangeState(new PigPatrolState());
        }
    }
}
