using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IAnimalState
{
    private Animal _animal;

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Death();

    public virtual void Exit() { }

    protected virtual void Death()
    {
        
    }
}