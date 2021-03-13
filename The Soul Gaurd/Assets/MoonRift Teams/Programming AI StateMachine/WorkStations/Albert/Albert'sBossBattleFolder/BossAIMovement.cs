using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIMovement : MonoBehaviour
{
    public Transform player;


    public float MoveSpeed = 4;
    public float rotateSpeed = 3;
    public float maxDist = 10;
    public float minDist = 5;
    public float attackTime;
    public float forwardTime;
    float pushBackTime = 0;
    public float pushDistance;

    public bool attacking;
    public bool followPlayer;
    bool hurt;
    bool forwardAttack;
    bool playerClose = false;

    Vector3 position;
    BossHealth bossHealth;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>();
        followPlayer = true;
    }
    // Update is called once per frame
    void Update()
    {

        //make boss not move through the x rotation
        if (followPlayer)
        {
            Path();
        }

        if (attacking)
        {
            Attack();
        }
        if (hurt && bossHealth.curHealth > 0)
        {
            pushBackTime += Time.deltaTime;
            transform.Translate(-Vector3.forward * 10 * Time.deltaTime);
            anim.SetTrigger("Hurt");
            attacking = false;
        }
        if(pushBackTime > pushDistance)
        {
            hurt = false;
        }

        if(bossHealth.curHealth <= 0)
        {
            anim.SetTrigger("Dead");
            followPlayer = false;
            attacking = false;
            // fix issue where when its 0 he sets the hurt animation THEN the dead animation
        }
    }

    void Path()
    {

        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) > maxDist)
        {
            playerClose = false;
            anim.SetBool("Walk", true);
        }
        if (Vector3.Distance(transform.position, player.position) >= minDist && playerClose == false)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            Debug.Log("far");
        }

        if (Vector3.Distance(transform.position, player.position) <= maxDist)
        {
            Debug.Log("Close");
            anim.SetBool("Walk", false);
            followPlayer = false;
            attacking = true;
        }


    }

    void Attack()
    {
        if (attacking)
        {
            attackTime += Time.deltaTime;
        }
        if (attackTime >= 1f)
        {
            anim.SetBool("ReadyAttack", true);
            //followPlayer = true;
            //attacking = false;
        }

        if(attackTime >= 2.5f && attackTime < 3 && hurt == false)
        {
            anim.SetBool("ReadyAttack", false);
            anim.SetTrigger("Attack");
            
            attackTime = 0;
            forwardAttack = true;
            forwardTime = 0;
        }

        if (forwardAttack)
        {
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);
            transform.Translate(Vector3.up * 5 * Time.deltaTime);
            forwardTime += Time.deltaTime;
        }
        if (forwardTime > .35f)
        {
            forwardAttack = false;
            attacking = false;
            followPlayer = true;
            forwardTime = 0;

        }
        //X rotation constraint is causing boss to keep staring at player
        // boss keeps looking at players foot

    }

    void Hit()
    {

    }

   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            pushBackTime = 0;
            Debug.Log("Hurt");
            hurt = true;
            anim.SetBool("ReadyAttack", false);
            anim.SetBool("Walk", false);

            bossHealth.takeDmg(10);
        }
    }
}
