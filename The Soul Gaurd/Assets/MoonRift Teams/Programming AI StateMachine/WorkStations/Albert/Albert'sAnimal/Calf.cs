using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calf : Animal
{
    public enum CALF_ANIMATIONSTATE
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
      JUMP,
      DIE,
      ATTACK_1,
      ATTACK_2,
      RUN_ATTACK
    }

    public CALF_ANIMATIONSTATE calfAnimationState;

    private ICalfState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new CalfIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetCalfAnimations();
    }

    public void SetCalfAnimations()
    {
        _anim.SetInteger("State", (int)calfAnimationState);
    }

    public void ChangeState(ICalfState newState)
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
            ChangeState(new CalfDeathState());
        }
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new CalfFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new CalfSleepState());
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
            ChangeState(new CalfChaseState());
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
            ChangeState(new CalfAttackState());
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
