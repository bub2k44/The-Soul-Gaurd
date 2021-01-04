using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doe : Animal
{
    public enum DOE_ANIMATIONSTATE
    {
        DOE_IDLE,
        DOE_IDLE_HEADSHAKE,
        DOE_DEATH,
        DOE_JUMP,
        DOE_SLEEP_START,
        DOE_SLEEP_END,
        DOE_WALK,
        DOE_HIT_FRONT,
        DOE_HIT_RIGHT,
        DOE_HIT_REAR,
        DOE_HIT_LEFT,
        DOE_WALK_SLOW,
        DOE_WALK_BACKWARDS,
        DOE_RUN,
        DOE_RUN_ATTACK,
        DOE_ATTACK_KICK,
        DOE_ATTACK_HEADBUTT,
        DOE_HATE,
        DOE_EAT_SEARCH,
        DOE_EAT
    }

    public DOE_ANIMATIONSTATE doeAnimationState;

    private IDoeState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new DoeIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetDoeAnimations();
    }

    public void SetDoeAnimations()
    {
        _anim.SetInteger("state", (int)doeAnimationState);
    }

    public void ChangeState(IDoeState newState)
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
        //    ChangeState(new DoeDeathState());
        //}
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new DoeFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new DoeSleepState());
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
            ChangeState(new DoeChaseState());
            */
        }
        //if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
        //    out hit, animalStats.lookRadius) && hit.collider.CompareTag("Doe"))
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
            ChangeState(new DoeAttackState());
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
