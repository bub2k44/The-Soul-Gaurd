using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Animal
{
    public enum BOSS_ANIMATION_STATE
    {
        WOLF_IDLE,
        WOLF_IDLE_LOOK,
        WOLF_IDLE_STANCE,
        WOLF_SEARCH_IDLE,
        WOLF_WALK_UPRIGHT,
        WOLF_SEARCH_WALK,
        WOLF_WALK_REGULAR,
        WOLF_WALK_BACKWARDS,
        WOLF_RUN,
        WOLF_SLEEP_START,
        WOLF_SLEEP_IDLE,
        WOLF_SLEEP_END,
        WOLF_EAT,
        WOLF_HIT_LEFT,
        WOLF_HIT_RIGHT,
        WOLF_HIT_FRONT,
        WOLF_HIT_BACK,
        WOLF_HOWL,
        WOLF_JUMP,
        WOLF_DEAD,
        WOLF_ATTACK
    }

    public BOSS_ANIMATION_STATE bossAnimationState;

    private IBossState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new BossIdleState());
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Execute();
        SetBossAnimation();
    }

    public void SetBossAnimation()
    {
        _anim.SetInteger("state", (int)BossAnimationState);
    }

    public void ChangeState(IBossState newState)
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
        //    ChangeState(new WolfDeathState());
        //}
    }

    //protected override void Thirst()
    //{
    //    base.Thirst();

    //    if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
    //    {
    //        ChangeState(new WolfFindWaterState());
    //    }
    //}

    //protected override void Sleepy()
    //{
    //    base.Sleepy();

    //    if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
    //    {
    //        ChangeState(new WolfSleepState());
    //    }
    //}

    protected override void Look()
    {
        base.Look();
        RaycastHit hit;

        if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
            out hit, animalStats.lookRadius) && hit.collider.CompareTag("Player"))
        {
            chaseTarget = hit.transform;
            FindTarget(chaseTarget);
            isPatrolState = false;
            ChangeState(new BossChaseState());
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
        if (other.gameObject.CompareTag("Player") && !isSleepState && !isDrinkState && !isAttackState && !isPlayState)
        {
            target = other.gameObject;
            ChangeState(new BossAttackState());
        }
        if (other.gameObject.CompareTag("Wolf") && !isSleepState && !isDrinkState && !isAttackState && !isPlayState)
        {
            target = other.gameObject;
        }

        //if (other.gameObject.CompareTag("Attack2") && !isTakeDamageState && currentHealth > 0)
        //{
        //    ChangeState(new WolfTakeDamageState());
        //}
    }

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
            //Gizmos.color = _currentState.sceneGizmoColor;
            //Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(eyes.position, animalStats.eyeRadius);
        }
    }
}