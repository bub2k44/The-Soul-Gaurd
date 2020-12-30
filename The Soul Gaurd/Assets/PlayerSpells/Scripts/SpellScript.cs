using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float speed;

    //private Transform target;
    public Transform MyTarget { get; set; }

    //private bool alive = true;

    //Jeremiah's Variables
    [SerializeField]
    private GameObject explosion;
    //[SerializeField]
    //private ParticleSystem explosion;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //target = GameObject.Find("RabbitAI").transform;

    }

    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            Vector3 direction = MyTarget.position - transform.position;
            rb.velocity = direction.normalized * speed;
        }

        //Vector3 direction = MyTarget.position - transform.position;
        //rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitBox") && other.transform == MyTarget)
        {
            Debug.Log("Impact");
            Instantiate(explosion, transform.position, Quaternion.identity);
            rb.velocity = Vector3.zero;
            MyTarget = null;
            Destroy(gameObject);
        }
    }
}
