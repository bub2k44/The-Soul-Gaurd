using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterIdleState : IdleState, IRoosterState
{
    private Rooster _rooster;

    public void Enter(Rooster rooster)
    {
        _rooster= rooster;
        _rooster.isIdleState = true;
        _rooster._navMeshAgent.speed = 0;//
    }

    public override void Exit()
    {
        _rooster.isIdleState = false;
    }

    protected override void Idle()
    {
        base.Idle();

        if (_idleType == 1)
        {
            _rooster.roosterAnimationState= Rooster.ROOSTER_ANIMATIONSTATE.ARM_COCK_IDLE_1;
        }
        else
        {
            _rooster.roosterAnimationState= Rooster.ROOSTER_ANIMATIONSTATE.ARM_COCK_IDLE_2;
            
        }

        if (_idleTimer >= _idleDuration)
        {
            _rooster.ChangeState(new RoosterPatrolState());
        }
    }
}
