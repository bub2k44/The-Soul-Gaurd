using UnityEngine;

public class DrinkState : IAnimalState
{
    private Animal _animal;

    protected float _drinkTimer;
    protected float _drinkDuration = Random.Range(4, 5);

    public void Enter(Animal animal)
    {
        _animal = animal;
    }

    public void Execute() => Eat();

    public virtual void Exit() { }

    protected virtual void Eat()
    {
        _drinkTimer += Time.deltaTime;
    }
}