using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerEvents : MonoBehaviour
{
    private AgentMovement agentMovement;

    private void Start()
    {
        agentMovement = GetComponentInParent<AgentMovement>();
    }

    public void StopMovementImmediatly()////
    {
        agentMovement.moveDirection = Vector3.zero;
    }

    public void SetFinsihedJumpingTrue()////
    {
        agentMovement.isFinishedJumping = true;
    }   
}
