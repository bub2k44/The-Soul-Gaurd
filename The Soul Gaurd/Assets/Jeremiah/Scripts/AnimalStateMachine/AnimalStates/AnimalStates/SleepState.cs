using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : IAnimalState
{
    private Animal _animal;

    protected float _sleepTimer;
    protected float _sleepDuration = Random.Range(4, 5);

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Sleeping();

    public virtual void Exit() { }

    protected virtual void Sleeping()
    {
        _sleepTimer += Time.deltaTime;
    }
}