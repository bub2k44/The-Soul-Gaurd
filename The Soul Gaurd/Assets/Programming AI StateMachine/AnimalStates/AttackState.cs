using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IAnimalState
{
    private Animal _animal;

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Attack();

    public virtual void Exit() 
    {

    }

    protected virtual void Attack()
    {

    }
}