using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Animal
{
    public enum SHEEP_ANIMATIONSTATE
    {
        ARM_SHEEP_IDLE_1,
        ARM_SHEEP_IDLE_2,
        ARM_SHEEP_WALK,
        ARM_SHEEP_WALK_BACK,
        ARM_SHEEP_RUN,
        ARM_SHEEP_JUMP,
        ARM_SHEEP_EAT_2,
        ARM_SHEEP_EAT_1,
        ARM_SHEEP_SLEEP_START,
        ARM_SHEEP_SLEEP_END,
        ARM_SHEEP_HATE,
        ARM_SHEEP_BEE,
        ARM_SHEEP_HIT_FRONT,
        ARM_SHEEP_HIT_LEFT,
        ARM_SHEEP_HIT_RIGHT,
        ARM_SHEEP_HIT_BACK,
        ARM_SHEEP_RUN_ATTACK,
        ARM_SHEEP_ATTACK_1,
        ARM_SHEEP_ATTACK_2,
        ARM_SHEEP_DEAD
    }

    public SHEEP_ANIMATIONSTATE sheepAnimationState;

    private ISheepState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new SheepIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetSheepAnimations();
    }

    public void SetSheepAnimations()
    {
        _anim.SetInteger("state", (int)sheepAnimationState);
    }

    public void ChangeState(ISheepState newState)
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
        //    ChangeState(new SheepDeathState());
        //}
    }

    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new SheepFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState &&
            !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new SheepSleepState());
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
            ChangeState(new SheepChaseState());
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
            ChangeState(new SheepAttackState());
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
