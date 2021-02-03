using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    protected Rigidbody rb;

    protected int damage;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected GameObject explosion;

    public Transform MyTarget { get; set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Initialize(Transform target, int damage)
    {
        MyTarget = target;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            Vector3 direction = MyTarget.position - transform.position;
            rb.velocity = direction.normalized * speed;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
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