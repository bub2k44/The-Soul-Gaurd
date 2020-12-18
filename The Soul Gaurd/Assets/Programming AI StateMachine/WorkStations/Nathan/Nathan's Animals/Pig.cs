using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Animal
{
    
    public enum PIG_ANIMATIONSTATE
    {
        PIG_IDLE_1,
        PIG_IDLE_2,
        PIG_WALK,
        PIG_WALK_BACK,
        PIG_WALK_SEARCH,
        PIG_RUN,
        PIG_SLEEP_START,
        PIG_SLEEP_END,
        PIG_EAT,
        PIG_EAT_2,
        PIG_HIT_LEFT,
        PIG_HIT_RIGHT,
        PIG_HIT_FRONT,
        PIG_HIT_TOP,
        PIG_CHROM_CHROM,
        PIG_SEARCH,
        PIG_DIE_2,
        PIG_DIE,
        PIG_ATTACK
    }

    public PIG_ANIMATIONSTATE pigAnimationState;

    private IPigState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new PigIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetPigAnimations();
    }

    public void SetPigAnimations()
    {
        _anim.SetInteger("state", (int)pigAnimationState);
    }

    public void ChangeState(IPigState newState)
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
            ChangeState(new PigDeathState());
        }
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && 
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new PigFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && 
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new PigSleepState());
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
            ChangeState(new PigChaseState());
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
            ChangeState(new PigAttackState());
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
