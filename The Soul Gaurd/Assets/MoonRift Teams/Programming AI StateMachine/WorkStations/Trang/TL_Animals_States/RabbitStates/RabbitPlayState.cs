using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitPlayState : PlayState, IRabbitState
{
    private Rabbit _rabbit;
    private float _playTimer;
    private float _playDuration;

    public void Enter(Rabbit rabbit)
    {
        _playDuration = 8;
        _rabbit = rabbit;
        _rabbit.FindTarget(_rabbit.target.transform);
        _rabbit.playFX.Play();
        _rabbit.isPlayState = true;
    }

    public void Execute() => Play();

    public void Exit()
    {
        _rabbit.playFX.Stop();
        _rabbit.isPlayState = false;
    }

    private void Play()
    {
        _rabbit._navMeshAgent.speed = 0;
        //_animal._anim.SetFloat("speed", 0);
        _playTimer += Time.deltaTime;

        if (_playTimer >= _playDuration)
        {
            //_animal.ChangeState(new PatrolState());
        }
    }
}
