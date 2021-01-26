using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : JumpState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        //controllerReference.myAnimations.TriggerFallAnimation();
        controllerReference.myAnimations.TriggerJumpAnimation();//
        controllerReference.movement.SetFinishJumpingFalse();
    }

    public override void Update()
    {
        base.Update();
    }
}
