﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IAnimalState
{
    private Animal _animal;

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Chase();

    public virtual void Exit() { }

    protected virtual void Chase()
    {
        
    }
}