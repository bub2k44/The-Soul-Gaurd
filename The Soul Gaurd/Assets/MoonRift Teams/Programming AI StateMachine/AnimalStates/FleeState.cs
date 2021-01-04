using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : IAnimalState
{
    private Animal _animal;

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Flee();

    public virtual void Exit() { }

    protected virtual void Flee()
    {

    }
}
