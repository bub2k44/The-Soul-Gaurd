using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAnimations : MonoBehaviour
{

    public Animator animator;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void TriggerLandingAnimation()
    {
        animator.SetTrigger("land");
    }

    public void ResetTriggerLandingAnimation()
    {
        animator.ResetTrigger("land");
    }

    public void TriggerJumpAnimation()
    {
        animator.SetTrigger("jump");
    }

    public void TriggerFallAnimation()
    {
        animator.SetTrigger("fall");
    }

    public void SetMovementFloat(float value)
    {
        animator.SetFloat("move", value);
    }

    public float SetCorrectAnimation(float desiredRotationAngle, int angleThreshold, int inputVerticalDirection)
    {
        float currentAnimationSpeed = animator.GetFloat("move");

        if (desiredRotationAngle > angleThreshold || desiredRotationAngle < -angleThreshold)
        {
            if (Mathf.Abs(currentAnimationSpeed) < 0.2f)
            {
                currentAnimationSpeed += inputVerticalDirection * Time.deltaTime * 2;
                currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, -0.2f, 0.2f);
            }

            SetMovementFloat(currentAnimationSpeed);
        }
        else
        {
            if (currentAnimationSpeed < 1)
            {
                currentAnimationSpeed += inputVerticalDirection * Time.deltaTime * 2;
            }

            SetMovementFloat(Mathf.Clamp(currentAnimationSpeed, -1, 1));
        }

        return Mathf.Abs(currentAnimationSpeed);
    }
}
