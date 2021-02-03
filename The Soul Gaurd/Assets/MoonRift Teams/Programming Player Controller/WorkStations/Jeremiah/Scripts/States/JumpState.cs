using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    bool landingTrigger = false;
    float delay = 0;

    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        delay = 0.2f;
        landingTrigger = false;
        controllerReference.myAnimations.ResetTriggerLandingAnimation();
        controllerReference.movement.HandleJump();
    }

    public override void Update()
    {
        base.Update();

        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        if (controllerReference.movement.IsGround())
        {
            if (landingTrigger == false)
            {
                landingTrigger = true;
                //controllerReference.movement.StartLandingAnimation();
                controllerReference.myAnimations.TriggerLandingAnimation();

                //controllerReference.movement.SetFinsihedJumping();//
            }
            if (controllerReference.movement.IsFinshedJumping())
            {
                controllerReference.TransitionState(controllerReference.movementState);
            }
        }

        //controllerReference.TransitionState(controllerReference.movementState);
    }
}
