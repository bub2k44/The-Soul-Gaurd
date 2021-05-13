using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float stunTime = 3f;
    bool canMove = true;
    public float speed = 2;

    void Update()
    {
        if(canMove == true)
        {
            transform.Translate(0, 0, Time.deltaTime * speed);
        }
    }
    IEnumerator Stun()
    {
        yield return new WaitForSeconds(stunTime);
        canMove = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        canMove = false;
        StartCoroutine(Stun());
    }
}
