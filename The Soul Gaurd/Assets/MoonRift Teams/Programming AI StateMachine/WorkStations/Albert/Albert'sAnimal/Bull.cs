using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : Animal
{
    public enum BULL_ANIMATIONSTATE
    {
      BULL_IDLE_1,
      BULL_IDLE_2,
      BULL_SLEEP_START,
      BULL_SLEEP_IDLE,
      BULL_SLEEP_END,
      BULL_WALK,
      BULL_RUN,
      BULL_DEAD,
      BULL_EAT,
      BULL_ATTACK,
      BULL_KA_KA,
      BULL_HIT,
    }

    public BULL_ANIMATIONSTATE bullAnimationState;

    private IBullState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new BullIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetBullAnimations();
    }

    public void SetBullAnimations()
    {
        _anim.SetInteger("State", (int)bullAnimationState);
    }

    public void ChangeState(IBullState newState)
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
            ChangeState(new BullDeathState());
        }
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new BullFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new BullSleepState());
        }
    }

    protected override void Look()
    {
        base.Look();
        RaycastHit hit;

        if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
            out hit, animalStats.lookRadius) && hit.collider.CompareTag("Wolf"))//Prey or Predator
        {
            chaseTarget = hit.transform;
            FindTarget(chaseTarget);
            isPatrolState = false;
            ChangeState(new BullChaseState());
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
            ChangeState(new BullAttackState());
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
