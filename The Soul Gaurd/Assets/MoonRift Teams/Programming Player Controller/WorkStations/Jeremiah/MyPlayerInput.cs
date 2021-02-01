using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerInput : MonoBehaviour
{
    public Vector2 MovementInputVector { get; private set; }
    public Vector3 MovementDirectionVector { get; private set; }

    private Camera mainCamera;

    public Action OnJump { get; set; }

    public GameObject player, bear, wolf, rabbit;// AA code
    public bool isPlayer = false, isBear = false, isWolf = false, isRabbit;// AA code

    public bool canJump = true;
    public Transform totemPosition;

    public MyAnimations myAnimations;

    private void Start()
    {
        mainCamera = Camera.main;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        GetMovementInput();
        GetMovementDirection();
        PlayerInput(); //AA Code
        Attack();//AA code
        GetJumpInput();

        if (Input.GetKeyDown(KeyCode.F))
        {
            isPlayer = true;
            isWolf = false;
            isBear = false;
            isRabbit = false;// AA code

            bear.SetActive(false);
            wolf.SetActive(false);
            player.SetActive(true);
            rabbit.SetActive(false);// AA code

            myAnimations.animator = transform.GetChild(0).GetComponent<Animator>();
            canJump = true;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            isBear = true;
            isPlayer = false;
            isWolf = false;
            isRabbit = false;// AA code

            bear.SetActive(true);
            player.SetActive(false);
            wolf.SetActive(false);
            rabbit.SetActive(false);// AA code
            myAnimations.animator = transform.GetChild(1).GetComponent<Animator>();
            canJump = false;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            isWolf = true;
            isPlayer = false;
            isBear = false;
            isRabbit = false;// AA code

            wolf.SetActive(true);
            player.SetActive(false);
            bear.SetActive(false);
            rabbit.SetActive(false);// AA code

            myAnimations.animator = transform.GetChild(2).GetComponent<Animator>();
            canJump = true;
        }
        // AA code
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRabbit = true;
            isPlayer = false;
            isWolf = false;
            isBear = false;

            rabbit.SetActive(true);
            player.SetActive(false);
            wolf.SetActive(false);
            bear.SetActive(false);

            myAnimations.animator = transform.GetChild(3).GetComponent<Animator>();
            canJump = true;

        }
    }
    //AA code
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myAnimations.TriggerBiteAnimation();
        }
        if (Input.GetMouseButtonDown(1))
        {
            myAnimations.TriggerSwipeAnimation();
        }
    }
    //AA code
    private void PlayerInput()
    {
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                transform.position = totemPosition.position;
            }
        }
    }


    private void GetJumpInput()
    {
        if (Input.GetAxisRaw("Jump") > 0 && canJump)//
        {
            OnJump?.Invoke();
        }
        //AA code
        // for bear
        if (Input.GetAxisRaw("Jump") > 0 && canJump == false)
        {
            myAnimations.standing = true;
            myAnimations.TriggerStandAnimation();


        }
    }

    private void GetMovementInput()
    {
        //MovementInputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        MovementInputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Debug.Log(MovementInputVector);
    }

    private void GetMovementDirection()
    {
        var cameraForwardDirection = mainCamera.transform.forward;
        //Debug.DrawRay(mainCamera.transform.position, cameraForwardDirection * 10, Color.red);
        MovementDirectionVector = Vector3.Scale(cameraForwardDirection, (Vector3.right + Vector3.forward));
        //Debug.DrawRay(mainCamera.transform.position, MovementDirectionVector * 10, Color.green);
    }
}
