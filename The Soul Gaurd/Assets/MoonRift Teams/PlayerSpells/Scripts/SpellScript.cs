using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float speed;

    //private Transform target;
    public Transform MyTarget { get; private set; }

    private int damage;

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

    public void Initialize(Transform target, int damage)
    {
        this.MyTarget = target;
        this.damage = damage;
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
            speed = 0;
            other.GetComponentInParent<Animal>().TakeDamage(damage);
            Debug.Log("Impact");
            Instantiate(explosion, transform.position, Quaternion.identity);
            rb.velocity = Vector3.zero;
            MyTarget = null;
            Destroy(gameObject);
        }
    }
}
