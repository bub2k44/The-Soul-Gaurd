public interface IAnimalState
{
    void Enter(Animal animal);

    void Execute();

    void Exit();
}