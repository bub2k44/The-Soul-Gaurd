using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IAnimalState
{
    private Animal _animal;

    protected float _patrolTimer;
    protected float _patrolDuration = Random.Range(4, 10);

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Patrol();

    public virtual void Exit() { }

    protected virtual void Patrol()
    {
        _patrolTimer += Time.deltaTime;       
    }
}