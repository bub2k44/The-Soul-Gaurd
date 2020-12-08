using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageState : IAnimalState
{
    private Animal _animal;

    protected bool _tookDamage;

    public void Enter(Animal animal)
    {
        _animal = animal;

        _tookDamage = false; 
    }

    public void Execute() => TakeDamage();

    public virtual void Exit() { }

    protected virtual void TakeDamage()
    {
    
    }
}