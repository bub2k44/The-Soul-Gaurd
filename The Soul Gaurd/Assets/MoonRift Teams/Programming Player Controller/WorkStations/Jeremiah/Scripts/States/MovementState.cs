using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : BaseState
{
    float defaultFallingDelay = .35f;
    float fallingDelay = 0;

    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        fallingDelay = defaultFallingDelay;
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.movement.HandleMovement(input);       
    }

    public override void HandleMovementDirection(Vector3 input)
    {
        base.HandleMovementDirection(input);
        controllerReference.movement.HandleMovementDirection(input);
    }

    public override void HandleJumpInput()
    {
        controllerReference.TransitionState(controllerReference.jumpState);
    }

    public override void Update()
    {
        base.Update();
        HandleMovement(controllerReference.input.MovementInputVector);
        HandleMovementDirection(controllerReference.input.MovementDirectionVector);

        if (controllerReference.movement.IsGround() == false)
        {
            if (fallingDelay > 0)
            {
                fallingDelay -= Time.deltaTime;
                return;
            }

            controllerReference.TransitionState(controllerReference.fallingState);
        }
        else
        {
            fallingDelay = defaultFallingDelay;
        }
    }
}
