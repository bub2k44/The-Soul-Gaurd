using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement2 : MonoBehaviour
{
    public Camera myCamera;

    public float speed = 2f;
    public float sprintSpeed = 5f;
    public float rotationSpeed = 15f;
    public float animationBlendSpeed = 6f;
    public float jumpSpeed = 7f;

    private CharacterController myController;
    private Animator anim;

    private float desiredRotation = 0f;
    private float desiredAnimationSpeed = 0f;

    private float speedY = 0f;
    private float gravity = -9.81f;

    private bool isSprinting = false;
    public bool isJumping = false;

    public bool canDoubleJump = false;
    public float doubleJumpMultiplier = .8f;

    private void Start()
    {
        myController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            anim.SetTrigger("jump");

            speedY += jumpSpeed;
        }

        if (!myController.isGrounded)
        {
            speedY += gravity * Time.deltaTime;
        }
        else if (speedY < 0)
        {
            speedY = 0;
        }

        anim.SetFloat("speedY", speedY / jumpSpeed);

        if (isJumping && speedY < 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                anim.SetTrigger("land");
            }
        }

        isSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(x, 0, z).normalized;

        Vector3 rotatedMovement = Quaternion.Euler(0, myCamera.transform.rotation.eulerAngles.y, 0) * movement;

        Vector3 verticalMovement = Vector3.up * speedY;

        myController.Move((verticalMovement + (rotatedMovement * (isSprinting ? sprintSpeed : speed))) * Time.deltaTime);

        if (rotatedMovement.magnitude > 0)
        {
            desiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            desiredAnimationSpeed = isSprinting ? 1 : 0.5f;
        }
        else
        {
            desiredAnimationSpeed = 0;
        }

        anim.SetFloat("speed", Mathf.Lerp(anim.GetFloat("speed"), desiredAnimationSpeed, animationBlendSpeed * Time.deltaTime));

        Quaternion currentRotation = transform.rotation;
        Quaternion targerRotation = Quaternion.Euler(0, desiredRotation, 0);

        transform.rotation = Quaternion.Lerp(currentRotation, targerRotation, rotationSpeed * Time.deltaTime);
    }
}
