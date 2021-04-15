using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerEvents : MonoBehaviour
{
    private AgentMovement agentMovement;
    AudioSource audio;
    public AudioClip footsteps1;
    private void Start()
    {
        agentMovement = GetComponentInParent<AgentMovement>();
        audio = GetComponentInParent<AudioSource>();

    }
    public void FootSteps()
    {
        audio.clip = footsteps1;
        audio.Play();
        
    }
}
