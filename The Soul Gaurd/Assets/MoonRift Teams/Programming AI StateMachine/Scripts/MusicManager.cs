using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [System.Serializable]
    public class TrackTable
    {
        public string trackName;
        public AudioClip track;
        public float volume = 0.5f;
    }

    public AudioSource musicAudioSource;
    public AudioSource ambianceAudioSource;
    public List<TrackTable> tracks = new List<TrackTable>();
    public List<TrackTable> ambianceTracks = new List<TrackTable>();

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void ChangeTrackWithoutFade(string tag)
    {
        var clipToPlay = tracks.Find(tracks => tracks.trackName == tag);
        musicAudioSource.clip = clipToPlay.track;
        musicAudioSource.Play();
        musicAudioSource.volume = clipToPlay.volume;
    }

    public void ChangeTrackForState(string state)
    {
        StartCoroutine(FadeOutAndPlayNextTrack(state));
    }

    public IEnumerator FadeOutAndPlayNextTrack(string state)
    {
        float currentTIme = 0;
        float start = musicAudioSource.volume;
        while (currentTIme < 2f)
        {
            currentTIme += Time.deltaTime;
            musicAudioSource.volume = Mathf.Lerp(start, 0, currentTIme / 2f);
            if(musicAudioSource.volume == 0)
            {
                musicAudioSource.Stop();
                ChangeTrackWithoutFade(state);
                musicAudioSource.volume = start;
                //audioSource.Play();
            }
            yield return null;
        }
        yield break;
    }
    public void ChangeAmbianceTrackWithoutFade(string tag)
    {
        var clipToPlay = ambianceTracks.Find(ambianceTracks => ambianceTracks.trackName == tag);
        ambianceAudioSource.clip = clipToPlay.track;
        ambianceAudioSource.Play();
        ambianceAudioSource.volume = clipToPlay.volume;
    }

    public void ChangeAmbianceTrackForState(string state)
    {
        StartCoroutine(FadeOutAndPlayNextTrack(state));
    }

    public IEnumerator FadeOutAndPlayNextAmbianceTrack(string state)
    {
        float currentTIme = 0;
        float start = ambianceAudioSource.volume;
        while (currentTIme < 2f)
        {
            currentTIme += Time.deltaTime;
            ambianceAudioSource.volume = Mathf.Lerp(start, 0, currentTIme / 2f);
            if (ambianceAudioSource.volume == 0)
            {
                ambianceAudioSource.Stop();
                ChangeTrackWithoutFade(state);
                ambianceAudioSource.volume = start;
                //audioSource.Play();
            }
            yield return null;
        }
        yield break;
    }
}
