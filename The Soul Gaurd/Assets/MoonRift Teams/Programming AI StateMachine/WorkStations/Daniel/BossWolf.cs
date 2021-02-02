using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BossWolf : Animal
{
    public enum WOLF_ANIMATION_STATE
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

    public WOLF_ANIMATION_STATE wolfAnimationState;

    private IWolfState _currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new WolfIdleState());
    }

    protected override void Update()
    {
        base.Update();
        Chase();
        _currentState.Execute();
        SetWolfAnimation();
    }

    public void SetWolfAnimation()
    {
        _anim.SetInteger("state", (int)wolfAnimationState);
    }

    public void ChangeState(IWolfState newState)
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
    /*
    protected override void Thirst()
    {
        base.Thirst();

        if (thirstDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new WolfFindWaterState());
        }
    }

    protected override void Sleepy()
    {
        base.Sleepy();

        if (awakeDuration <= 0 && !isIdleState && !isSleepState && !isDrinkState && !isAttackState && !isPlayState && !isFindWaterState && !isChaseState)
        {
            ChangeState(new WolfSleepState());
        }
    }
    */
    void Chase()
    {
        FindTarget(player.transform);
        ChangeState(new WolfChaseState());
    }
    /*
    protected override void Look()
    {
        base.Look();
        RaycastHit hit;

        if (Physics.SphereCast(eyes.position, animalStats.sphereCastRadius, eyes.forward,
            out hit, animalStats.lookRadius) && hit.collider.CompareTag("Bear"))
        {
            chaseTarget = hit.transform;
            FindTarget(chaseTarget);
            isPatrolState = false;
            ChangeState(new WolfChaseState());
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
    */
    protected void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !isSleepState && !isDrinkState && !isAttackState && !isPlayState)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //player.gameObject.SetActive(false);
            //target = other.gameObject;
            //ChangeState(new WolfAttackState());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Attack2") )
        {
            ChangeState(new WolfPatrolState());
        }
    }

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