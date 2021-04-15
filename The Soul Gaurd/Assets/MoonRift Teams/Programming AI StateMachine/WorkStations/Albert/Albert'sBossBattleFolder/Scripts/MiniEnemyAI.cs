using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MiniEnemyAI : MonoBehaviour
{
    public Transform player, spawnPosition;
    public GameObject chaseCollider;
    public float MoveSpeed = 4;
    bool follow;
    NavMeshAgent nav;
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
      
            Path();
        

            //nav.enabled = false;
        
    }

    public void Path()
    {

        nav.SetDestination(player.position);
        anim.SetBool("Walk", true);
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            
            Debug.Log("Hurt");
            anim.SetBool("Walk", false);
        }
    }
}
