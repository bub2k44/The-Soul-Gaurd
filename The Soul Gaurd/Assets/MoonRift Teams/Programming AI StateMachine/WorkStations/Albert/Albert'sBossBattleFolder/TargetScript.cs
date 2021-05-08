using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public enum Team
    {
        one,
        two,
    }

    public Team team;

    //Combat Stats
    public float health;
    public float meleeAttDMG;
    public float meleeImpactForce;

    //Targeting
    public float maxTargeting;
    public bool targetingCapacity;
    public float targetedBy;
    public bool targetingCombatant;
    public bool targetFound;
    public bool isDead;
    public bool targetCheckReady;
    public float targetingValue;
    public float targetingTick;
    public AttackRadius meleeAttackRadius;
    public bool playerFound;

    //Player References
    public GameObject player;
    public bool playerOnMap;
    public float playerDist;
    public float playerRange;
    public bool targetingPlayer;

    //Movement
    UnityEngine.AI.NavMeshAgent agent;


    //Combat Function
    public bool isMeleeAttacking;
    public GameObject meleeAttackBox;
    public Rigidbody rb;
    public float bodyPersistTimer;
    public float knockBackResistance;
    public bool isHit;
    public float isHitDelay;
    public float originalSpeed;


    //Hit Flash
    [SerializeField] private Renderer myObject;
    public float flashTime;
    Color originalColor;

    //Audio
    public bool meleeAttackAudioPlayed;
    public AudioSource combatantHitWav, combatantDeathWav, combatantAttackWav, combatantGroupFoundWav;

    //Animation
    private Animator combatantAnim;

    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        originalSpeed = agent.speed;
        originalColor = myObject.material.color;
        player = GameObject.FindWithTag("Player");
        meleeAttackBox = this.GetComponentInChildren<MeleeCombatantHitBox>().gameObject;
        rb = GetComponent<Rigidbody>();
        combatantAnim = gameObject.GetComponent<Animator>();
        targetCheckReady = true;
        targetingCapacity = false;
        //Team Identification
        var outlineColor = GetComponent<Outline>();
        if (team == Team.one)
        {
            transform.gameObject.tag = "TeamOne";
            outlineColor.OutlineColor = new Color32(0, 255, 0, 255);
            gameObject.AddComponent<TeamOne>();

        }
        if (team == Team.two)
        {
            transform.gameObject.tag = "TeamTwo";
            outlineColor.OutlineColor = new Color32(255, 0, 0, 255);
            gameObject.AddComponent<TeamTwo>();

        }
        meleeAttackRadius = this.GetComponentInChildren<AttackRadius>();
    }

    void FixedUpdate()
    {
        //Detecting Player
        if (GameObject.FindWithTag("Player") == null)
        {
            playerOnMap = false;
        }
        else
        {
            playerOnMap = true;
        }

        FindClosestEnemy();
        MeleeAttack();

        //KnockBackBegins
        if (rb.isKinematic == false)
        {
            StartCoroutine("KnockBackRecovery");
        }

        if (isDead)
        {
            meleeAttackBox.SetActive(false);
        }
    }

    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        if (!isDead)
        {
            if (team == Team.one)
            {

                if (!targetFound)
                {
                    TeamTwo closestEnemy = null;
                    TeamTwo[] allCombatants = GameObject.FindObjectsOfType<TeamTwo>();

                    foreach (TeamTwo currentEnemy in allCombatants)
                    {
                        TargetScript enemyCheck = (currentEnemy.GetComponentInParent<TargetScript>());
                        if (enemyCheck.targetingCapacity == false && enemyCheck.isDead == false)
                        {
                            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                            if (distanceToEnemy < distanceToClosestEnemy)
                            {
                                distanceToClosestEnemy = distanceToEnemy;
                                closestEnemy = currentEnemy;
                                targetFound = true;
                                agent.SetDestination(closestEnemy.transform.position);
                                combatantAnim.SetFloat("move", 1);
                            }
                        }
                    }

                    if (closestEnemy != null)
                    {
                        if (!targetingCombatant)
                        {
                            TargetScript combatant = closestEnemy.GetComponentInParent<TargetScript>();
                            combatant.Targeting(targetingValue);
                            targetingCombatant = true;
                        }

                        if (closestEnemy.GetComponentInParent<TargetScript>().isDead)
                        {
                            TargetScript deadTarget = closestEnemy.GetComponentInParent<TargetScript>();
                            deadTarget.Targeting(-1);
                            targetingCombatant = false;
                            closestEnemy = null;
                            deadTarget = null;
                            targetFound = false;
                        }
                    }
                    else
                    {
                        targetingCombatant = false;
                    }
                }

                if (targetCheckReady)
                {
                    StartCoroutine("TargetingCheck");
                }
            }

            //Team Two
            if (team == Team.two)
            {
                //Check for Player
                if (player != null)
                {
                    playerDist = Vector3.Distance(player.transform.position, transform.position);
                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                    if (playerHealth.isDead)
                    {
                        targetingPlayer = false;
                    }

                    if (playerDist <= playerRange && playerHealth.targetingCapacity == false && !targetingPlayer && !targetingCombatant && !playerHealth.isDead) //Wider Bubble Check, like is on same island/continent check more for later use
                    {
                        playerHealth.Targeting(targetingValue);
                        targetingPlayer = true;
                        targetFound = true;
                    }
                    else if (playerDist > playerRange)
                    {
                        targetingPlayer = false;
                    }
                }

                if (!targetingPlayer)
                {
                    if (!targetFound)
                    {
                        TeamOne closestEnemy = null;
                        TeamOne[] allCombatants = GameObject.FindObjectsOfType<TeamOne>();


                        foreach (TeamOne currentEnemy in allCombatants)
                        {
                            TargetScript enemyCheck = (currentEnemy.GetComponentInParent<TargetScript>());
                            if (enemyCheck.targetingCapacity == false && enemyCheck.isDead == false)
                            {
                                float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                                if (distanceToEnemy < distanceToClosestEnemy)
                                {
                                    distanceToClosestEnemy = distanceToEnemy;
                                    closestEnemy = currentEnemy;
                                    targetFound = true;
                                    agent.SetDestination(closestEnemy.transform.position);
                                    combatantAnim.SetFloat("move", 1);
                                }
                            }
                        }

                        if (closestEnemy != null)
                        {

                            if (!targetingCombatant)
                            {
                                TargetScript combatant = closestEnemy.GetComponentInParent<TargetScript>();
                                combatant.Targeting(targetingValue);
                                targetingCombatant = true;
                            }

                            if (closestEnemy.GetComponentInParent<TargetScript>().isDead)
                            {
                                TargetScript deadTarget = closestEnemy.GetComponentInParent<TargetScript>();
                                deadTarget.Targeting(-1);
                                targetingCombatant = false;
                                closestEnemy = null;
                                deadTarget = null;
                                targetFound = false;
                            }
                        }
                        else
                        {
                            targetingCombatant = false;
                        }
                    }
                }

                if (targetingPlayer)
                {
                    agent.SetDestination(player.transform.position);
                    combatantAnim.SetFloat("move", 1);
                    //If distance is short on check distance then check/add if player capacity for targeting is true. If true then leave alone and player is not targeted. Change PlayerFound to playerTargeted
                    //If false, then move to player, change to running, and then on the attackradius script it should detect player and attacks should damage player just like other raptors
                }

                if (targetCheckReady)
                {
                    StartCoroutine("TargetingCheck");
                }
            }
        }
    }
    public void Targeting(float targeting)
    {
        targetedBy += targeting;
        if (targetedBy >= maxTargeting)
        {
            targetingCapacity = true;
        }
        else
        {
            targetingCapacity = false;
        }
    }
    private IEnumerator TargetingCheck()
    {
        targetCheckReady = false;
        yield return new WaitForSeconds(0.15f);
        targetCheckReady = true;
        targetFound = false;
        targetedBy -= targetingTick;
        if (targetedBy < maxTargeting)
        {
            targetingCapacity = false;
        }
        // if (targetingPlayer)
        // {
        //     targetingPlayer = false;
        // }
    }

    private void MeleeAttack()
    {
        if (!isDead)
        {
            if (meleeAttackRadius.isMeleeAttacking == true)
            {
                isMeleeAttacking = true;
                combatantAnim.SetFloat("move", 0);
                agent.speed = originalSpeed / 4;
            }
            else if (meleeAttackRadius.isMeleeAttacking == false)
            {
                isMeleeAttacking = false;
                agent.speed = originalSpeed;
            }

            if (isMeleeAttacking)
            {
                meleeAttackBox.SetActive(true);
                if (!meleeAttackAudioPlayed)
                {
                    combatantAttackWav.Play();
                    meleeAttackAudioPlayed = true;
                }
                combatantAnim.speed = 2f;
                combatantAnim.SetTrigger("bite");
            }
            else
            {
                combatantAnim.SetTrigger("bite");
                meleeAttackBox.SetActive(false);
                combatantAnim.speed = 1f;
                meleeAttackAudioPlayed = false;
            }
        }
    }
    //Combat Scripts
    public void TakeDamage(float amount)
    {
        if (!isHit && !isDead)
        {
            combatantAnim.SetFloat("move",0);
            combatantAnim.SetTrigger("Hurt");
            rb.isKinematic = false;
            health -= amount;
            StartCoroutine(Hit());
            combatantHitWav.Play();
            if (health <= 0f)
            {
                isDead = true;
                combatantAnim.SetTrigger("Death");
                combatantAnim.SetTrigger("Hurt");
                StartCoroutine(Die());
            }
        }
    }
    private IEnumerator Hit()
    {
        //particle effect of being hit with melee stars here, instantiated
        isHit = true;
        myObject.material.color = Color.yellow;
        yield return new WaitForSeconds(isHitDelay);
        myObject.material.color = originalColor;
        isHit = false;
    }

    private IEnumerator Die()
    {
        transform.gameObject.tag = "Defeated";
        combatantAnim.SetFloat("move", 0);
        isDead = true;
        rb.isKinematic = false;
        combatantAnim.SetTrigger("Death");
        combatantDeathWav.Play();
        yield return new WaitForSeconds(0.3f);
        Destroy(agent);
        yield return new WaitForSeconds(bodyPersistTimer);
        Destroy(gameObject);
    }

    private IEnumerator KnockBackRecovery()
    {
        yield return new WaitForSeconds(knockBackResistance);
        rb.isKinematic = true;
        combatantAnim.SetTrigger("Hurt");
    }
}
