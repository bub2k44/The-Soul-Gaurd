using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSoundPlayer : MonoBehaviour
{
    public AudioSource footStepAudioSource;
    public AudioClip footStepsClip;
    float lastTime = 0;
    float duration;

    private void Start()
    {
        duration = footStepsClip.length;
    }

    public void PlayeFootstepSound()
    {
        if (lastTime == 0)
        {
            footStepAudioSource.PlayOneShot(footStepsClip);
        }
        if (Time.time - lastTime >= duration)
        {
            lastTime = Time.time;
            footStepAudioSource.PlayOneShot(footStepsClip);
        }
    }
}
