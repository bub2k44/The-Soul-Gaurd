using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IAnimalState
{
    private Animal _animal;

    protected float _alertTimer;
    protected float _alertDuration = 10f;

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Alert();

    public virtual void Exit() { }

    protected virtual void Alert()
    {
        _alertTimer += Time.deltaTime;
    }
}
