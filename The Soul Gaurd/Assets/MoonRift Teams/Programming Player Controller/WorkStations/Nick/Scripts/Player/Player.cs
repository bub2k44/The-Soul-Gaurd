using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }

            return instance;
        }
    }

    public enum PLAYER_ANIMATION_STATES
    {
        PLAYER_IDLE,
        PLAYER_WALK,
        PLAYER_RUN,
        PLAYER_HIT,
        PLAYER_JUMP,
        PLAYER_DEAD,
        PLAYER_ATTACK
    }

    public PLAYER_ANIMATION_STATES playerAnimationState;
    public ParticleSystem attackFX;
    public ParticleSystem possessionFX;

    public Animator animtemp;
    public Rigidbody rb;
    public GameObject SimpleCameraAngle, Target;
    private float cameraVertRot = 0, cameraHorRot = 0;
   

    [SerializeField]
    private BasicStats basicStats;
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    //Jeremiah's Variables
    //[SerializeField]
    //private Stat health;

    private bool isAttacking = false;
    private Coroutine attackRoutine;
    //private SpellBook spellBook;
    //[SerializeField]
    //private GameObject[] spellPrefabs;

    public Transform MyTarget { get; set; }

    protected override void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Anim = animtemp;//GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        //Jeremiah's Code

        //MyHealth.Initialized(basicStats.health, basicStats.maxHealth);//TO DO

        //MyTarget = GameObject.Find("RabbitAI").transform;
        //spellBook = GetComponent<SpellBook>();
    }

    private GameObject holder;

    private void Update()
    {
        InputHandlerPosession();
        InputHandlerAttack();
        SetPlayerAnimation();

        //Jeremiah's Code
        if (Input.GetKeyDown(KeyCode.I))
        {
            MyHealth.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            MyHealth.MyCurrentValue += 10;

        }

        foreach (string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }

        //InLineOfSight();
    }

    private void SetPlayerAnimation()
    {
        animtemp.SetInteger("state", (int)playerAnimationState);
    }

    private void InputHandlerPosession()
    {
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
                    Target.GetComponent<Possession>().Possess();
                    possessionFX.Play();
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
    }

    private void InputHandlerAttack()
    {
        if (InputHandler.attack || InputHandler.secondaryAttack)
        {
            switch (basicStats.name)
            {
                case "Human":
                    if (InputHandler.attack)
                    {
                        animtemp.SetBool("FullAuto_b", false);
                    }
                    else
                    {
                        animtemp.SetBool("FullAuto_b", true);
                    }
                    animtemp.SetBool("Shoot_b", true);
                    break;
                case "Fox":
                    break;
                case "Rabbit":
                    break;
                default:
                    break;
            }
        }
        else if (!InputHandler.attack && !InputHandler.secondaryAttack)
        {
            animtemp.SetBool("Shoot_b", false);
            animtemp.SetBool("FullAuto_b", false);
        }
    }

    private void FixControl()
    {
        holder.GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = true;
    }

    private void FixedUpdate()
    {
        //BasicStatsNameSwitch();

        Vector3 move = new Vector3(InputHandler.MovementInput[0], 0, InputHandler.MovementInput[1]);
        //Debug.Log(move);
        transform.position += transform.forward * InputHandler.MovementInput[1] * Time.deltaTime * basicStats.movespeed;
        transform.position += transform.right * InputHandler.MovementInput[0] * Time.deltaTime * basicStats.movespeed;
        //float playerSpeed = 1.0f;
        //transform.position += (move * Time.deltaTime * basicStats.movespeed);

        AlbertsAnimatioSolution(move);

        CameraRotation();

        //gameObject.transform.eulerAngles = new Vector3(0.0f, gameObject.transform.eulerAngles.y, 0.0f);
        animtemp.SetFloat("Speed_f", InputHandler.MovementInput[1]);
        animtemp.SetFloat("AbsSpeed_f", Mathf.Abs(InputHandler.MovementInput[1]));
    }

    private void CameraRotation()
    {
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
    }

    //private void BasicStatsNameSwitch()
    //{
    //    switch (basicStats.name)
    //    {
    //        case "Human":
    //            //if (Target!=null)
    //            //gameObject.SetActive(false);

    //            break;
    //        case "Fox":
    //            animtemp.SetBool("Eat_b", InputHandler.special);
    //            break;
    //        case "Rabbit":
    //            animtemp.SetBool("Eat_b", InputHandler.special);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    private void AlbertsAnimatioSolution(Vector3 move)
    {
        if (move.magnitude > 0)
        {
            //animtemp.SetInteger("state", 5);
            playerAnimationState = PLAYER_ANIMATION_STATES.PLAYER_WALK;
        }
        else
        {
            //animtemp.SetInteger("state", 0);
            playerAnimationState = PLAYER_ANIMATION_STATES.PLAYER_IDLE;
        }
    }
    //Jeremiah's Functions
    private IEnumerator Attack(string spellName)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = SpellBook.MyInstance.CastSpell(spellName);
        isAttacking = true;
        yield return new WaitForSeconds(newSpell.MyCastTime);

        if (currentTarget != null)//&& InLineOfSight()
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, transform.position, Quaternion.identity).GetComponent<SpellScript>();
            s.Initialize(currentTarget, newSpell.MyDamage);
        }

        
        StopAttack();
    }

    public void StopAttack()
    {
        SpellBook.MyInstance.StopCasting();

        isAttacking = false;

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }
    }

    public void CastSpell(string spellName)
    {
        if (MyTarget != null && !isAttacking)
        {
            //StartCoroutine(Attack(spellIndex));
            attackRoutine = StartCoroutine(Attack(spellName));
        }
    }
    
    //private bool InLineOfSight()
    //{
    //    Vector3 targetDirection = (target.transform.position - transform.position).normalized;
    //    //var distance = Vector2.Distance(transform.position, target.transform.position);
    //    //Debug.DrawRay(transform.position, targetDirection, Color.red);
    //    //RaycastHit hit = Physics.Raycast(transform.position, targetDirection, Vector3.Distance(transform.position, target.transform.position));

    //    if (hit.collider == null)
    //    {
    //        return true;
    //    }

    //    return false;
    //}


    private void JeremiahsAnimationSolution()
    {
        //
    }
}
