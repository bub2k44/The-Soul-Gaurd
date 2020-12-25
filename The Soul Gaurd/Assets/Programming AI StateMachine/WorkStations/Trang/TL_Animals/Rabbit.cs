using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal
{

    public enum RABBIT_ANIMATIONSTATE
    {
        ARM_RABBIT_IDLE_1,
        ARM_RABBIT_IDLE_2,
        ARM_RABBIT_EAT,
        ARM_RABBIT_RUN,
        ARM_RABBIT_JUMP,
        ARM_RABBIT_WALK,
        ARM_RABBIT_HIT_LEFT,
        ARM_RABBIT_HIT_RIGHT,
        ARM_RABBIT_HIT_FRONT,
        ARM_RABBIT_DEAD
    }

    public RABBIT_ANIMATIONSTATE rabbitAnimationState;

    private IRabbitState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new RabbitIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetRabbitAnimations();
    }

    public void SetRabbitAnimations()
    {
        _anim.SetInteger("state", (int)rabbitAnimationState);
    }

    public void ChangeState(IRabbitState newState)
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
            ChangeState(new RabbitDeathState());
        }
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new RabbitFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new RabbitSleepState());
        }
    }

    protected override void Look()
    {
        base.Look();
        RaycastHit hit;

       /* if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
            out hit, animalStats.lookRadius) && hit.collider.CompareTag("Wolf"))
        {
            chaseTarget = hit.transform;
            FindTarget(chaseTarget);
            isPatrolState = false;
            ChangeState(new BearChaseState());
        }*/
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

  /*  protected void OnTriggerEnter(Collider other)
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
    }*/

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
