using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenState : IAnimalState
{
    private Animal _animal;
    protected float _wakeUpTimer;
    protected float _wakeUpDuration = Random.Range(4, 5);

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Awaken();

    public virtual void Exit() { }

    protected virtual void Awaken()
    {
        _wakeUpTimer += Time.deltaTime;
    }
}