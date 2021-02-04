using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : Character//mono
{
    private static AgentController instance;

    public static AgentController MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AgentController>();
            }

            return instance;
        }
    }

    //Jeremiah's Variables
    //[SerializeField]
    //private Stat health;

    private bool isAttacking = false;
    private Coroutine attackRoutine;
    //private SpellBook spellBook;
    //[SerializeField]
    //private GameObject[] spellPrefabs;

    public Transform MyTarget { get; set; }


    public AgentMovement movement;

    public MyPlayerInput input;
    public MyAnimations myAnimations;

    BaseState currentState;
    public readonly BaseState movementState = new MovementState();
    public readonly BaseState jumpState = new JumpState();
    public readonly BaseState fallingState = new FallingState();

    private void OnEnable()
    {
        movement = GetComponent<AgentMovement>();

        input = GetComponent<MyPlayerInput>();
        myAnimations = GetComponent<MyAnimations>();
        //input = GetComponentInParent<MyPlayerInput>();
        //myAnimations = GetComponentInParent<MyAnimations>();
        currentState = movementState;
        currentState.EnterState(this);
        AssignMovementInputListeners();
    }

    private void AssignMovementInputListeners()
    {
        input.OnJump += HandleJump;
    }

    private void HandleJump()
    {
        currentState.HandleJumpInput();
    }

    private void Update()
    {
        currentState.Update();

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
    }

    private void OnDisable()
    {
        input.OnJump -= currentState.HandleJumpInput;
    }

    public void TransitionState(BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
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

    //AA code
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossHitBox"))
        {
            //lose health
            MyHealth.MyCurrentValue -= 10;

            myAnimations.TriggerHurtAnimation();
        }
    }
}