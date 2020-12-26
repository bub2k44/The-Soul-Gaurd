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

    private void FootStepsGrass1()
    {
        SoundManager.PlaySound("SG_Grass_Walk_1");
    }

    private void FootStepsGrass2()
    {
        SoundManager.PlaySound("SG_Grass_Walk_2");
    }
}
