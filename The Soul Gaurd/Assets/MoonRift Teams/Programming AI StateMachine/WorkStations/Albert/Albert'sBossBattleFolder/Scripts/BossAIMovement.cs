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
    float attackTime;
    float forwardTime;
    float hurtTime;
    float pushBackTime = 0;
    public float pushDistance;
    public Material whiteFlash;
    public Material originalMaterial;
    public float flashTime;


    public bool attacking;
    public bool followPlayer;
    bool stunned;
    bool hurt;
    bool forwardAttack;
    bool playerClose = false;
    bool startMatch;
    Vector3 position;
    BossHealth bossHealth;
    BoxCollider collider;
    Rigidbody rb;
    AudioSource audio;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>();
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider>();
        collider.enabled = true;
        rb.useGravity = enabled;
        followPlayer = false ;
        startMatch = true;
        anim.SetBool("Walk", false);
        transform.LookAt(player);
    }

   
    // Update is called once per frame
    void Update()
    {
        StartMatch();
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
            if(pushBackTime > .1f)
            {
                anim.SetTrigger("Hurt");

            }
            stunned = true;
            attacking = false;
        }
        if(pushBackTime > pushDistance)
        {
            hurt = false;
        }
        if (stunned)
        {
            hurtTime += Time.deltaTime;
        }

        if(hurtTime > 2)
        {
            stunned = false;
            followPlayer = true;
            anim.SetBool("ReadyAttack", false) ;
            hurtTime = 0;
        }
        if(bossHealth.curHealth <= 0)
        {
            anim.SetTrigger("Dead");
            followPlayer = false;
            attacking = false;
            // fix issue where when its 0 he sets the hurt animation THEN the dead animation
        }
    }
    void StartMatch()
    {
        if (startMatch)
        {
            attackTime += Time.deltaTime;

            if (attackTime > 3)
            {
                followPlayer = true;
                startMatch = false;

                attackTime = 0;
            }
        }
        
    }
    void Path()
    {
        
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) > maxDist)
        {
            playerClose = false;
            anim.SetBool("Walk", true);
            anim.SetBool("ReadyAttack", false);
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
        if (attackTime >= .5f)
        {
            anim.SetBool("ReadyAttack", true);
            //followPlayer = true;
            //attacking = false;
        }

        if(attackTime >= 1.45f && attackTime < 2 && hurt == false)
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
            //transform.Translate(Vector3.up * 5 * Time.deltaTime);
            forwardTime += Time.deltaTime;
        }
        if (forwardTime > .6f)
        {
            forwardAttack = false;
            attacking = false;
            followPlayer = true;
            forwardTime = 0;

        }
        //X rotation constraint is causing boss to keep staring at player
        // boss keeps looking at players foot

    }

    public void DeathOfPlayer()
    {
        followPlayer = false;
        attacking = false;
        //Looks at player
        // transform.LookAt(player);
        

        
    }
   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            pushBackTime = 0;
            Debug.Log("Hurt");
            audio.Play();
            hurt = true;
            anim.SetBool("ReadyAttack", false);
            anim.SetBool("Walk", false);

            bossHealth.takeDmg(10);
            FlashWhite();
        }

        if (other.gameObject.CompareTag("Attack2"))
        {
            followPlayer = true;
            Debug.Log("Start");
        }
       
    }
    void FlashWhite()
    {
        GetComponentInChildren<Renderer>().material = whiteFlash;
        Invoke("ResetColor", flashTime);
    }
    void ResetColor()
    {
        GetComponentInChildren<Renderer>().material = originalMaterial;
    }
}
