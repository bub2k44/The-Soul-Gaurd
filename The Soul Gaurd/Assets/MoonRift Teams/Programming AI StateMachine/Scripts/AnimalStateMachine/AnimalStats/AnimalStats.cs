using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="DefaultAnimalStats")]
public class AnimalStats : ScriptableObject
{
    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 5f;

    public float lookRadius = 40f;
    public float attackRadius = 1f;
    public float sphereCastRadius = 5f;
    public float eyeRadius = 1f;

    public float awakeDuration = 30f;
    public float thirstDuration = 15f;

    public int maxHealth = 100;
}
