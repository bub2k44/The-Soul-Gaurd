using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseScript : MonoBehaviour
{
    public Transform player, spawnPosition;
    public GameObject chaseCollider;
    public float MoveSpeed = 4;
    public bool follow;
    public NavMeshAgent nav;
    BossAIMovement ai;
    public ThirdPersonMovement playerScript;
    public Animator anim;
    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ai = GetComponent<BossAIMovement>();

        follow = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerScript.dead == false)
        {
            Path();
        }
        else
        {
            nav.enabled = false;
        }
    }

    public void Path()
    {
       
            nav.SetDestination(player.position);
            anim.SetBool("Walk", true);   
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Enables end chase
        if (other.gameObject.CompareTag("PlayerSwitch"))                                                
        {
            transform.position = spawnPosition.position;// Transforms boss to spawn point
            follow = false;
            ai.enabled = true;//enables AI script
            nav.enabled = false;
            Destroy(chaseCollider);
            this.enabled = false;
        }
    }
}
