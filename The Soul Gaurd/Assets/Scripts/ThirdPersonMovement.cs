﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CameraController cc;
    public ChaseScript chase;
    public Transform cam;
    public CharacterController controller;
    public Stat health;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    int checkpoint;
    Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    bool isGrounded;
    public bool dead = false;
    public bool hurt; //AA code
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        health.Initialized(100, 100);
        checkpoint = PlayerPrefs.GetInt("Checkpoint");
    }
    // Update is called once per frame
    void Update()
    {
        if (dead == false) 
        {
            Movement();
            Gravity();
            Jump();
            Attack();//AA code
           
        }
        if (health.MyCurrentValue <= 0)
        {
            animator.SetTrigger("death");
            dead = true;
        }

    }
    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        animator.SetFloat("move", direction.magnitude);
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("jump");
        }
    }

    //AA code
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("bite");
        }
    }

    //AA code
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossHitBox"))
        {
            //lose health
            hurt = true;
            animator.SetTrigger("Hurt");
            health.MyCurrentValue -= 50;

        }

        if (other.gameObject.CompareTag("ChaseHitBox"))
        {
            animator.SetTrigger("death");
            health.MyCurrentValue -= 100;

        }

        if (other.gameObject.CompareTag("Jump"))
        {
            //cc.thirdPersonCam.m_Priority = 1;
            checkpoint = 1;
            PlayerPrefs.SetInt("Checkpoint", checkpoint);
            chase.follow = false;
            chase.timeStart = true;
        }
        
    }
    /*
    private void Awake()
    {
        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("bite");
        }
    }
    */
}