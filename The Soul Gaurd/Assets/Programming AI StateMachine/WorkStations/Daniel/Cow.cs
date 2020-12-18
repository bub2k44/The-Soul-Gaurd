using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Animal

{
    public enum COW_ANIMATIONSTATE
    {
        COW_IDLE,
        COW_IDLE_SEARCH,
        COW_EAT,
        COW_RUN,
        COW_WALK,
        COW_HIT,
        COW_ATTACK_HEADBUTT,
        COW_PEE,
        COW_DEATH,
        COW_SLEEP_END,
        COW_SLEEP_START
    }

    public COW_ANIMATIONSTATE cowAnimationState;

    private ICowState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new CowIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetCowAnimations();
    }

    public void SetCowAnimations()
    {
        _anim.SetInteger("state", (int)cowAnimationState);
    }

    public void ChangeState(ICowState newState)
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
            //isFleeState = false;
            ChangeState(new CowDeathState());
        }
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState)//&& !isFleeState
        {
            ChangeState(new CowFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState)//&& !isFleeState
        {
            ChangeState(new CowSleepState());
        }
    }

    protected override void Look()
    {
        base.Look();
        RaycastHit hit;

        //if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
        //    out hit, animalStats.lookRadius) && (hit.collider.CompareTag("Large Predator") || hit.collider.CompareTag("Apex Predator")))
        //{
        //    chaseTarget = hit.transform;
        //    FleeTarget(chaseTarget);
        //    isPatrolState = false;
        //    ChangeState(new CowFleeState());
        //}
        //if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
        //    out hit, animalStats.lookRadius) && hit.collider.CompareTag("Cow"))
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
        //if ((other.gameObject.CompareTag("Large Predator") || other.gameObject.CompareTag("Apex Predator")) && !isSleepState &&
        //    !isDrinkState && !isAttackState && !isPlayState)
        //{
        //    Transform target = other.gameObject.transform;
        //    FleeTarget(target);
        //    ChangeState(new CowFleeState());
        //}
        //if ((other.gameObject.CompareTag("Large Prey") || other.gameObject.CompareTag("Small Prey") || other.gameObject.CompareTag("Small Predator")) && !isSleepState &&
        //    !isDrinkState && !isAttackState && !isPlayState)
        //{
        //    target = other.gameObject;
        //}
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Attack2") && !isTakeDamageState && currentHealth > 0)
    //    {
    //        ChangeState(new WolfTakeDamageState());
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == null)
        {
            ChangeState(new CowPatrolState());
        }
    }

    private void OnDrawGizmos()
    {
        if (_currentState != null && eyes != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(eyes.position, animalStats.eyeRadius);
        }
    }
}
