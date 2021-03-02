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
    
    bool playerClose = false;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Path();
    }

    void Path()
    {

        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) > maxDist)
        {
            playerClose = false;
        }
        if (Vector3.Distance(transform.position, player.position) >= minDist && playerClose == false)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
           

            Debug.Log("far");
        }

        if (Vector3.Distance(transform.position, player.position) <= maxDist)
        {
            
            Debug.Log("Close");
            
        }


    }
}
