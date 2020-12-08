using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimationTriggerEvents : MonoBehaviour
{
    private Animal animal;

    private void Start()
    {
        animal = GetComponent<Animal>();
    }

    private void Attack()
    {
        animal.attackFX.Play();
    }

    private void FootSteps()
    {

    }
}
