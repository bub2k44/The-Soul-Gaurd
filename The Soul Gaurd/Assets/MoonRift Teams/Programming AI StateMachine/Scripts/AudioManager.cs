using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Static Instance
    private static AudioManager instance;
    public static AudioManager Instance 
    { 
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();

                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    #endregion

    #region Fields
    private AudioSource musicSource1;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;
    #endregion

    private void Awake()
    {
        //Make sure we do not destroy this instance
        DontDestroyOnLoad(this.gameObject);

        //Create audio sources, and save them as references
        musicSource1 = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        //Loop the music tracks
        musicSource1.loop = true;
        musicSource2.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        //Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource1 : musicSource2;//Check

        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }

    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        //Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource1 : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        //Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource1 : musicSource2;//Check
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource1;//Check

        //Swap the source
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        //Set the fileds of the audio source, then start te coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime = 1.0f)
    {
        //Make sure source is active and playing
        if (!activeSource.isPlaying)
        {
            activeSource.Play();
        }

        float transition = 0.0f;

        //Fade out
        for (transition = 0; transition < transitionTime; transition += Time.deltaTime)
        {
            activeSource.volume = (1 - (transition / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        //Fade in
        for (transition = 0; transition < transitionTime; transition += Time.deltaTime)
        {
            activeSource.volume = (transition / transitionTime);
            yield return null;
        }
    }

    private IEnumerator UpdateMusicWithCrossFade(AudioSource originalSource, AudioSource newSource, float transitionTime)
    {
        float transition = 0.0f;

        for (transition = 0.0f; transition <= transitionTime; transition += Time.deltaTime)
        {
            originalSource.volume = (1 - (transition / transitionTime));
            newSource.volume = (transition / transitionTime);
            yield return null;
        }

        originalSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource1.volume = volume;
        musicSource2.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
