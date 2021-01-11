using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
    public enum BEAR_ANIMATIONSTATE
    {
        BEAR_IDLE, 
        BEAR_IDLE_HEADSHAKE, 
        BEAR_IDLE_UPRIGHT, 
        BEAR_IDLE_SEARCH, 
        BEAR_WALK, 
        BEAR_WALK_SEARCH_DOWN, 
        BEAR_WALK_BACK, 
        BEAR_RUN, 
        BEAR_SLEEP_START, 
        BEAR_SLEEP_END, 
        BEAR_EAT, BEAR_HIT,
        BEAR_HIT_UPRIGHT,
        BEAR_UP, BEAR_DOWN,
        BEAR_GROWL,
        BEAR_GROWL_UPRIGHT,
        BEAR_DEAD_SIDE, 
        BEAR_DEAD_UPRIGHT_FORWARD, 
        BEAR_DEAD_UPRIGHT_SIDE,
        BEAR_ATTACK_BITE,
        BEAR_ATTACK_SWIPE, 
        BEAR_ATTACK_UPRIGHT_BITE,
        BEAR_ATTACK_UPRIGHT_SWIPE
    }

    public BEAR_ANIMATIONSTATE bearAnimationState;

    private IBearState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new BearIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetBearAnimations();
    }

    public void SetBearAnimations()
    {
        _anim.SetInteger("state", (int)bearAnimationState);
    }

    public void ChangeState(IBearState newState)
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

        if (MyHealth.MyCurrentValue <= 0)
        {
            isAlertState = false;
            isAttackState = false;
            isAwakenState = false;
            isChaseState = false;
            isDeathState = true;
            isDrinkState = false;
            isFindWaterState = false;
            isFleeState = false;
            isIdleState = false;
            isPatrolState = false;
            isPlayState = false;
            isSleepState = false;
            isTakeDamageState = false;
            ChangeState(new BearDeathState());
        }
        //if (currentHealth <= 0)
        //{
        //    isAttackState = false;
        //    isIdleState = false;
        //    isPatrolState = false;
        //    ChangeState(new BearDeathState());
        //}
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && 
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new BearFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && 
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new BearSleepState());
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
            ChangeState(new BearChaseState());
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
            ChangeState(new BearAttackState());
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