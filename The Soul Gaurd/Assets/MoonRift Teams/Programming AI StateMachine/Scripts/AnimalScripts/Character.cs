using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected Transform hitBox;

    [SerializeField]
    private Stat health;

    public Stat MyHealth { get => health; }

    [SerializeField]
    private float initHealth = 100;

    protected virtual void Start()
    {
        health.Initialized(initHealth, initHealth);
    }

    public virtual void TakeDamage(float damage)
    {
        MyHealth.MyCurrentValue -= damage;

        //if (MyHealth.MyCurrentValue <= 0)
        //{
            
        //}
    }
}
