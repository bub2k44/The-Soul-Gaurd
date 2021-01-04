using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{
    public enum FOX_ANIMATIONSTATE
    {
        FOX_IDLE,
        FOX_IDLE_ALT,
        FOX_HIT_FRONT,
        FOX_HIT_RIGHT,
        FOX_HIT_REAR,
        FOX_HIT_LEFT,
        FOX_DEATH,
        FOX_HATE,
        FOX_ATTACK,
        FOX_ATTACK_ALT,
        FOX_HOWL,
        FOX_EAT,
        FOX_EAT_SEARCH,
        FOX_SEARCH_MOVING,
        FOX_RUN,
        FOX_JUMP,
        FOX_WALK,
        FOX_WALK_BACKWARDS,
        FOX_WALK_SLOW,
        FOX_SLEEP_START,
        FOX_SLEEP_END
    }

    public FOX_ANIMATIONSTATE foxAnimationState;

    private IFoxState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new FoxIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetFoxAnimations();
    }

    public void SetFoxAnimations()
    {
        _anim.SetInteger("state", (int)foxAnimationState);
    }

    public void ChangeState(IFoxState newState)
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
        //    ChangeState(new FoxDeathState());
        //}
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new FoxFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new FoxSleepState());
        }
    }

    protected override void Look()
    {
        base.Look();
        RaycastHit hit;

        if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
            out hit, animalStats.lookRadius) && hit.collider.CompareTag("Wolf"))
        {
            /*chaseTarget = hit.transform;
            FindTarget(chaseTarget);
            isPatrolState = false;
            ChangeState(new FoxChaseState());*/
        }
        //if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
        //    out hit, animalStats.lookRadius) && hit.collider.CompareTag("Fox"))
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
            ChangeState(new FoxAttackState());
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
