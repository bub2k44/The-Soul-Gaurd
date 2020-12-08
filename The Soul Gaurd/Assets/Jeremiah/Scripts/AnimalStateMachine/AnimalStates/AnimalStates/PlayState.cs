using UnityEngine;

public class PlayState : IAnimalState
{
    private Animal _animal;
    private float _playTimer;
    private float _playDuration;

    public void Enter(Animal animal)
    {
        _playDuration = 8;
        _animal = animal;
        _animal.FindTarget(_animal.target.transform);
        _animal.playFX.Play();
        _animal.isPlayState = true;
    }

    public void Execute() => Play();

    public void Exit()
    {
        _animal.playFX.Stop();
        _animal.isPlayState = false;
    }

    private void Play()
    {
        _animal._navMeshAgent.speed = 0;
        //_animal._anim.SetFloat("speed", 0);
        _playTimer += Time.deltaTime;

        if (_playTimer >= _playDuration)
        {
            //_animal.ChangeState(new PatrolState());
        }
    }
}