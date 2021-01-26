using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    protected CharacterController characterController;
    protected MyAnimations agentAnimations;

    public float movementSpeed;
    public float jumpSpeed;
    public float gravity;
    public float rotationSpeed;

    public int angleThreshold;
    public Vector3 moveDirection = Vector3.zero;
    protected float desiredRotationAngle = 0;
    int inputVerticalDirection = 0;
    bool isJumping = false;
    public bool isFinishedJumping = true;

    public BasicStats playerStats;
    public BasicStats bearStats;
    public BasicStats wolfStat;

    public MyPlayerInput myPlayerInput;
    //public GameObject player, bear, wolf;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        agentAnimations = GetComponent<MyAnimations>();
        myPlayerInput = GetComponent<MyPlayerInput>();
    }
    private void Update()
    {
        if (myPlayerInput.isPlayer)
        {
            movementSpeed = playerStats.movespeed;
            jumpSpeed = playerStats.jumpSpeed;
        }
        if (myPlayerInput.isBear)
        {
            movementSpeed = bearStats.movespeed;
            jumpSpeed = bearStats.jumpSpeed;
        }
        if (myPlayerInput.isWolf)
        {
            movementSpeed = wolfStat.movespeed;
            jumpSpeed = wolfStat.jumpSpeed;
        }

        if (characterController.isGrounded)
        {         
            if (moveDirection.magnitude > 0 && isFinishedJumping)//
            {
                var animationSpeedMultiplier = agentAnimations.SetCorrectAnimation(desiredRotationAngle, angleThreshold, inputVerticalDirection); ;

                RotateAgent();

                moveDirection *= animationSpeedMultiplier;                
            }
        }

        moveDirection.y -= gravity;

        if (isJumping)
        {
            isJumping = false;
            isFinishedJumping = false;
            moveDirection.y = jumpSpeed;
            agentAnimations.SetMovementFloat(0);
            agentAnimations.TriggerJumpAnimation();
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    public bool IsGround()
    {
        return characterController.isGrounded;
    }

    public void HandleMovement(Vector2 input)
    {
        if (characterController.isGrounded)
        {
            if (input.y != 0)
            {
                if (input.y > 0)
                {
                    
                    inputVerticalDirection = Mathf.CeilToInt(input.y);
                }
                else
                {
                    inputVerticalDirection = Mathf.FloorToInt(input.y);
                }

                moveDirection = input.y * transform.forward * movementSpeed;
            }
            else
            {
                agentAnimations.SetMovementFloat(0);
                moveDirection = Vector3.zero;
            }
        }
    }

    public void HandleMovementDirection(Vector3 input)
    {
        desiredRotationAngle = Vector3.Angle(transform.forward, input);
        var crossProduct = Vector3.Cross(transform.forward, input).y;

        if (crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }
    }

    public void HandleJump()
    {
        if (characterController.isGrounded)
        {
            isJumping = true;
        }
    }



    private void RotateAgent()
    {
        if (desiredRotationAngle > angleThreshold || desiredRotationAngle < -angleThreshold)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
        }
    }

    //public void StopMovementImmediatly()////
    //{
    //    moveDirection = Vector3.zero;
    //}

    public void StartLandingAnimation()
    {
        agentAnimations.TriggerLandingAnimation();
    }

    public bool IsFinshedJumping()
    {
        return isFinishedJumping;
    }

    //public void SetFinsihedJumping()////
    //{
    //    isFinishedJumping = true;
    //}

    public void SetFinisgJumping(bool value)
    {
        isFinishedJumping = value;
    }

    public void SetFinishJumpingFalse()
    {
        isFinishedJumping = false;
    }
}
