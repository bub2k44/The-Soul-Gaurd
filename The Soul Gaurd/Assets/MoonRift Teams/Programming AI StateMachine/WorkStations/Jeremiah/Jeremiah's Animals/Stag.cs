using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stag : Animal
{
    public enum STAG_ANIMATIONSTATE
    {
        IDLE_1,
        IDLE_2,
        WALK_1,
        WALK_BACK,
        WALK_SLOW,
        RUN,
        SLEEP_START,
        SLEEP_IDLE,
        SLEEP_END,
        EAT_1,
        EAT_2,
        HIT_LEFT,
        HIT_RIGHT,
        HIT_FRONT,
        HIT_BACK,
        HATE,
        STAG_JUMP,
        DIE,
        ATTACK_1,
        ATTACK_2,
        RUN_ATTACK
    }

    public STAG_ANIMATIONSTATE stagAnimationState;

    private IStagState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new StagIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetStagAnimation();
    }

    public void SetStagAnimation()
    {
        _anim.SetInteger("state", (int)stagAnimationState);
    }

    public void ChangeState(IStagState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter(this);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //if (currentHealth <= 0)
        //{
        //    isAttackState = false;
        //    isIdleState = false;
        //    isPatrolState = false;
        //    ChangeState(new StagDeathState());//
        //}
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new StagFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new StagSleepState());
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
            ChangeState(new StagChaseState());
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
            ChangeState(new StagAttackState());
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
