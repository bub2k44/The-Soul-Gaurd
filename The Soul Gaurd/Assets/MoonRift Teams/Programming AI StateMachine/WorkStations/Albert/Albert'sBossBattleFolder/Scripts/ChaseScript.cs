using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScript : MonoBehaviour
{
    public Transform player, spawnPosition;
    public GameObject chaseCollider;
    float maxDist = 3;
    float minDist = 1.5f;
    public float MoveSpeed = 4;
    public float startTime;
    public float chaseTime;
    bool follow;
    bool timeStart;
    BossAIMovement ai;
    Rigidbody rb;
    BoxCollider collider;
    public Animator anim;
    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        ai = GetComponent<BossAIMovement>();
        follow = true;
    }
    // Update is called once per frame
    void Update()
    {
        chaseTime += Time.deltaTime;
        if (follow)
        {
            anim.SetBool("Walk", true);
            //Looks at player
            transform.LookAt(player);

            //Not close to player, so still follows
            if (Vector3.Distance(transform.position, player.position) > maxDist)
            {
                anim.SetBool("Walk", true);
            }
            //player is too far, continues to follow
            if (Vector3.Distance(transform.position, player.position) >= minDist)
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;

                Debug.Log("far");
            }

            //player is close
            if (Vector3.Distance(transform.position, player.position) <= maxDist)
            {
                Debug.Log("Close");
                anim.SetBool("Walk", false);
            }

        }

        //Destroys this script and enables AI script
        if (Input.GetKeyDown(KeyCode.P))
        {
            follow = false;
            timeStart = true;
           

            // transform.position = spawnPosition.position;// Transforms boss to spawn point
        }

        if (timeStart)
        {
            startTime += Time.deltaTime;
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);//translatesboss forward
            transform.Translate(Vector3.up * 10 * Time.deltaTime);//translates boss up making him jump
        }
        if(startTime > 3)
        {
            collider.enabled = true;
            rb.useGravity = true;// enables gravity
            rb.drag = -5;//Input drag amount
        }

        if(startTime > 3.5f)
        {
            rb.drag = 0;//Input drag amount
        }
      

        if (startTime > 5)
        {

            ai.enabled = true;//enables AI script
            Destroy(chaseCollider);
            Destroy(this);//Destroys this script
        }

        //Change time according to length of chase
        if(chaseTime > 4)
        {
            collider.enabled = true;//enables collider
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Enables end chase
        if (other.gameObject.CompareTag("Jump"))                                                
        {
            follow = false;
            timeStart = true;

        }


    }
}
