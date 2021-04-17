using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    float timeSinceAttack;
    public float treeTime;
    int checkpoint;
    Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    bool isGrounded;
    bool inTrees;
    public bool dead = false;
    public bool hurt; //AA code
    public bool arenaReached = false;
    Animator animator;
    public CinemachineVirtualCamera rabbitCam;
    public CinemachineFreeLook wolfCam;
    public GameObject rabbit, fightText, fightCollider;
    public GameObject wolf;
    AudioSource audio;
    public AudioClip bite;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        health.Initialized(100, 100);
        audio = GetComponent<AudioSource>();
        checkpoint = PlayerPrefs.GetInt("Checkpoint");
    }
    // Update is called once per frame
    void Update()
    {
        if (dead == false) 
        {
            timeSinceAttack += Time.deltaTime;
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
        if (dead == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (inTrees)
        {
            treeTime += Time.deltaTime;
        }
        if(treeTime > 1.5f)
        {
            health.MyCurrentValue = 0;
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
            transform.rotation = Quaternion.Euler(0f, angle, 0f) ;
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
        if (Input.GetMouseButtonDown(0) && timeSinceAttack > 1)
        {
            animator.SetTrigger("bite");
            timeSinceAttack = 0;
        }
    }

    //AA code
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossHitBox"))
        {
            //lose health
            hurt = true;
            audio.clip = bite;
            audio.Play();
            animator.SetTrigger("Hurt");
            health.MyCurrentValue -= 15;

        }

        if (other.gameObject.CompareTag("ChaseHitBox"))
        {
            animator.SetTrigger("death");
            audio.clip = bite;

            audio.Play();
            health.MyCurrentValue -= 100;

        }

        if (other.gameObject.CompareTag("Jump"))
        {
            //cc.thirdPersonCam.m_Priority = 1;
            PlayerPrefs.SetInt("CheckPoint", 1);
            fightCollider.SetActive(true);
            arenaReached = true;
            other.gameObject.SetActive(false);
            //chase.follow = false;
            //chase.timeStart = true;
        }
        if (other.gameObject.CompareTag("PlayerSwitch"))
        {
            rabbitCam.m_Priority = 0;
            wolfCam.m_Priority = 1;
            rabbit.SetActive(false);
            wolf.SetActive(true);
            animator = GetComponentInChildren<Animator>();

        }
        if (other.gameObject.CompareTag("Fight"))
        {
            fightText.SetActive(true);
        }
        if (other.gameObject.CompareTag("TreeLine"))
        {
            inTrees = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fight"))
        {
            fightText.SetActive(false);
        }
        if (other.gameObject.CompareTag("TreeLine"))
        {
            inTrees = false;
            treeTime = 0;
        }
    }



}
