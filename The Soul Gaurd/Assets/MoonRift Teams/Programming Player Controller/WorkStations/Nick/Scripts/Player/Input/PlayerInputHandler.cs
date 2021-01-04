using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    //public Camera camera;
    public Rigidbody rb;
    public bool Airborne = false, Possession = false, special = false, attack = false, secondaryAttack = false;
    public Animator animtemp;
    public Vector2 MovementInput { get; private set; }
    public Vector2 CameraMovementInput { get; private set; }

    public Player player; 

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!special)
        {
            MovementInput = context.ReadValue<Vector2>();
            player.playerAnimationState = Player.PLAYER_ANIMATION_STATES.PLAYER_WALK;
        }
        else
        {
            MovementInput = new Vector2(0, 0);
            player.playerAnimationState = Player.PLAYER_ANIMATION_STATES.PLAYER_IDLE;
        }
        //Debug.Log(MovementInput);
    }

    public void OnCameraMoveInput(InputAction.CallbackContext context)
    {
        CameraMovementInput = context.ReadValue<Vector2>();
        //Debug.Log(CameraMovementInput);
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started && Airborne==false)
        {
            Airborne = true;
            rb.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
            Debug.Log("Jump button pushed down now");
            //animtemp.SetBool("Jump_b", true);
            player.playerAnimationState = Player.PLAYER_ANIMATION_STATES.PLAYER_JUMP;
        }
        if (context.performed)
        {
            Debug.Log("Jump is being held down");
            player.playerAnimationState = Player.PLAYER_ANIMATION_STATES.PLAYER_JUMP;
        }
        if (context.canceled)
        {
            Debug.Log("Jump button has been released");
        }
    }

    public void OnSpecialInput(InputAction.CallbackContext context)
    {
        if (context.started && !(Mathf.Abs(MovementInput[0])>0 || Mathf.Abs(MovementInput[1]) > 0))
            special = !special;
            /*if (special == false)
                special = true;
            else
                special = false;*/
    }
    public void OnPossessionInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Possession = !Possession;
            //player.possessionFX.Play();
        }
        /*
         * set possession to true; then in player script: if type != Human, unpossess (for test, instantiate player at transform);
         * if type == human, raycast forward and if target is animal (later if bellow %hp), destroy self, set animal's SimpleCameraAngle to active (contains c
         */
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!secondaryAttack)
            {
                attack = true;
                //player.playerAnimationState = Player.PLAYER_ANIMATION_STATES.PLAYER_ATTACK;
            }
        }
        else if (context.performed)
        {
            player.playerAnimationState = Player.PLAYER_ANIMATION_STATES.PLAYER_ATTACK;
        }
        else if (context.canceled)
        {
            attack = false;
            //player.playerAnimationState = Player.PLAYER_ANIMATION_STATES.PLAYER_IDLE;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!attack)
            {
                secondaryAttack = true;
            }
        }
        else if (context.canceled)
        {
            secondaryAttack = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Airborne = false;
            //animtemp.SetBool("Jump_b", false);
            Debug.Log("Grounded");
            player.playerAnimationState = Player.PLAYER_ANIMATION_STATES.PLAYER_IDLE;
        }
    }
}
