using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//new
public class Player : MonoBehaviour
{
    public Animator animtemp;
    public Rigidbody rb;
    public GameObject SimpleCameraAngle, Target;
    private float cameraVertRot = 0, cameraHorRot = 0;
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    [SerializeField]
    private BasicStats basicStats;
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, basicStats, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, basicStats, "move");
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Anim = animtemp;//GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine.Initialize(IdleState);
    }
    private GameObject holder;

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        if (InputHandler.Possession)
        {
            switch (basicStats.name)
            {
                case "Human":
                    if (Target == null)
                    {
                        InputHandler.Possession = false;
                        break;
                    }
                    Destroy(gameObject);
                    Target.GetComponent<AIHunterEvasion>().Possess();
                    break;
                default:
                    GameObject Ai = Instantiate(Resources.Load("Prefabs/" + basicStats.name + "AI") as GameObject);
                    Ai.transform.position = transform.position;
                    Ai.transform.rotation = transform.rotation;
                    Destroy(gameObject);
                    GameObject PlayerAnimal = Instantiate(Resources.Load("Prefabs/Player") as GameObject);
                    PlayerAnimal.transform.position = transform.position;
                    PlayerAnimal.transform.rotation = transform.rotation;
                    PlayerAnimal.transform.position += new Vector3(-3, 1, 0);
                    PlayerAnimal.GetComponent<Player>().cameraHorRot = cameraHorRot;
                    PlayerAnimal.GetComponent<Player>().cameraVertRot = cameraVertRot;
                    break;
            }
        }
        if (InputHandler.attack || InputHandler.secondaryAttack)
        {
            switch (basicStats.name)
            {
                case "Human":
                    break;
                case "Fox":
                    break;
                case "Rabbit":
                    break;
                default:
                    break;
            }
        }
    }
    private void FixControl()
    {
        holder.GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = true;
    }
    private void FixedUpdate()
    {
        switch (basicStats.name)
        {
            case "Human":
                //if (Target!=null)
                //gameObject.SetActive(false);

                break;
            case "Fox":
                animtemp.SetBool("Eat_b", InputHandler.special);
                break;
            case "Rabbit":
                animtemp.SetBool("Eat_b", InputHandler.special);
                break;
            default:
                break;
        }
        StateMachine.CurrentState.PhysicsUpdate();
        Vector3 move = new Vector3(InputHandler.MovementInput[0], 0, InputHandler.MovementInput[1]);
        //Debug.Log(move);
        transform.position += transform.forward * InputHandler.MovementInput[1] * Time.deltaTime * basicStats.movespeed;
        transform.position += transform.right * InputHandler.MovementInput[0] * Time.deltaTime * basicStats.movespeed;
        //float playerSpeed = 1.0f;
        //transform.position += (move * Time.deltaTime * basicStats.movespeed);

        cameraHorRot -= InputHandler.CameraMovementInput[1];
        cameraVertRot += InputHandler.CameraMovementInput[0];
        Quaternion cameraMove = Quaternion.Euler(/*cameraHorRot*/transform.rotation.x, cameraVertRot, transform.rotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraMove, Time.deltaTime * 5.0f);

        //Quaternion CameraMoveHorizontal = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        //SimpleCameraAngle.transform.rotation = Quaternion.Slerp(SimpleCameraAngle.transform.rotation,CameraMoveHorizontal,Time.deltaTime*5.0f);
        if (cameraHorRot > 43)
        {
            cameraHorRot = 43;
        }
        if (cameraHorRot < -43)
        {
            cameraHorRot = -43;
        }
        SimpleCameraAngle.transform.eulerAngles = new Vector3(cameraHorRot, cameraVertRot, 0.0f);
        //gameObject.transform.eulerAngles = new Vector3(0.0f, gameObject.transform.eulerAngles.y, 0.0f);
        animtemp.SetFloat("Speed_f", InputHandler.MovementInput[1]);
        animtemp.SetFloat("AbsSpeed_f", Mathf.Abs(InputHandler.MovementInput[1]));
    }
}
