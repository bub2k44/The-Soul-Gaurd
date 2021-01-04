using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose : Animal
{
    
    public enum MOOSE_ANIMATIONSTATE
    {
        MOOSE_IDLE_1,
        MOOSE_IDLE_2,
        MOOSE_WALK_1,
        MOOSE_WALK_BACK,
        MOOSE_WALK_SLOW,
        MOOSE_RUN,
        MOOSE_SLEEP_START,
        MOOSE_SLEEP_END,
        MOOSE_EAT_1,
        MOOSE_EAT_2,
        MOOSE_HIT_LEFT,
        MOOSE_HIT_RIGHT,
        MOOSE_HIT_FRONT,
        MOOSE_HIT_BACK,
        MOOSE_HATE,
        MOOSE_WALK_WOUNDED,
        MOOSE_DIE,
        MOOSE_ATTACK_1,
        MOOSE_ATTACK_2,
        MOOSE_RUN_ATTACK
    }

    public MOOSE_ANIMATIONSTATE mooseAnimationState;

    private IMooseState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new MooseIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetMooseAnimations();
    }

    public void SetMooseAnimations()
    {
        _anim.SetInteger("state", (int)mooseAnimationState);
    }

    public void ChangeState(IMooseState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter(this);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (currentHealth <= 0)
        {
            isAttackState = false;
            isIdleState = false;
            isPatrolState = false;
            ChangeState(new MooseDeathState());
        }
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && 
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new MooseFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && 
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new MooseSleepState());
        }
    }

    protected override void Look()
    {
        base.Look();
        RaycastHit hit;

        if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
            out hit, animalStats.lookRadius) && hit.collider.CompareTag("Wolf"))
        {
            chaseTarget = hit.transform;
            FindTarget(chaseTarget);
            isPatrolState = false;
            ChangeState(new MooseChaseState());
        }
        //if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
        //    out hit, animalStats.lookRadius) && hit.collider.CompareTag("Bear"))
        //{
        //    chaseTarget = hit.transform;
        //    FindTarget(chaseTarget);
        //    //////////////////
        //    isPatrolState = false;
        //    ChangeState(new PlayState());
        //}
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wolf") && !isSleepState &&
            !isDrinkState && !isAttackState && !isPlayState)
        {
            target = other.gameObject;
            ChangeState(new MooseAttackState());
        }
        if (other.gameObject.CompareTag("Bear") && !isSleepState &&
            !isDrinkState && !isAttackState && !isPlayState)
        {
            target = other.gameObject;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Attack2") && !isTakeDamageState && currentHealth > 0)
    //    {
    //        ChangeState(new WolfTakeDamageState());
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Attack2"))
    //    {
    //        ChangeState(new WolfPatrolState());
    //    }
    //}

    private void OnDrawGizmos()
    {
        if (_currentState != null && eyes != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(eyes.position, animalStats.eyeRadius);
        }
    }
}
