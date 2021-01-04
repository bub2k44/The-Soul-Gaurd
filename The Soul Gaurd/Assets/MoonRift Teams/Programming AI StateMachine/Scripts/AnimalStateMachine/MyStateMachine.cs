using UnityEngine;

public class MyStateMachine : MonoBehaviour
{
    protected IAnimalState _state;

    public void SetState(IAnimalState state)
    {
        _state = state;
    }
}