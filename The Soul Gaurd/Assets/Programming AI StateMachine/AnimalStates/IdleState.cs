using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IAnimalState
{
    private Animal _animal;

    protected float _idleTimer;
    protected float _idleDuration = Random.Range(4, 5);
    protected int _idleType = Random.Range(0, 2);

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Idle();

    public virtual void Exit() { }

    protected virtual void Idle()
    {
        _idleTimer += Time.deltaTime;
    }
}