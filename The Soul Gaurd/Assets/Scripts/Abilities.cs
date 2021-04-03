using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public Rigidbody stunProjectile;
    public Transform spellSpawn;
    public float spellSpeed = 350f;
    
   

    // Update is called once per frame
    void Update()
    {
        StunSpell();
    }

    void StunSpell()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody stun;
            stun = Instantiate(stunProjectile, spellSpawn.position, spellSpawn.rotation) as Rigidbody;
            stun.AddForce(spellSpawn.forward * spellSpeed);
        }
    }
}
