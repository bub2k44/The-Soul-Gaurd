using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemyAI : MonoBehaviour
{
    public Transform player;


    public float MoveSpeed = 4;
    public float rotateSpeed = 3;
    public float maxDist = 10;
    public float minDist = 5;
    float hurtTime;
    float pushBackTime = 0;
    float attackTime;
    public float pushDistance;
    public int lives = 3;

    public bool attacking;
    public bool followPlayer;
    bool stunned;
    bool hurt;
    bool forwardAttack;
    bool playerClose = false;

    Vector3 position;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

        
        if (hurt /*&& bossHealth.curHealth > 0*/)
        {
            --lives;
            pushBackTime += Time.deltaTime;
            transform.Translate(-Vector3.forward * 10 * Time.deltaTime);
            if(pushBackTime > .1f)
            {
                anim.SetTrigger("Hurt");
            }
            stunned = true;
        }
        if (pushBackTime > pushDistance)
        {
            hurt = false;
        }
        if (stunned)
        {
            hurtTime += Time.deltaTime;
        }

        if (hurtTime > 2)
        {
            stunned = false;
            followPlayer = true;
            hurtTime = 0;
        }
        
        if (lives <= 0)
        {
            anim.SetTrigger("Dead");
            followPlayer = false;
           
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
            attackTime += Time.deltaTime;
            if(attackTime > 2 && hurt == false)
            {
                anim.SetTrigger("Attack");
                attackTime = 0;
            }
        }


    }

   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            pushBackTime = 0;
            Debug.Log("Hurt");
            hurt = true;
            anim.SetBool("Walk", false);
        }
    }
}
