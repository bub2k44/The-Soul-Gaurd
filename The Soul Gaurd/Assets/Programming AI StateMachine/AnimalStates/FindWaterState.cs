using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWaterState : IAnimalState
{
    private Animal _animal;

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => FindWater();

    public virtual void Exit() { }

    protected virtual void FindWater()
    {
        
    }
}