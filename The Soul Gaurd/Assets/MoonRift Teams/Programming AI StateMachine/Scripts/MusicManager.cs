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

    AudioSource audioSource;
    public List<TrackTable> tracks = new List<TrackTable>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void ChangeTrackWithoutFade(string tag)
    {
        var clipToPlay = tracks.Find(tracks => tracks.trackName == tag);
        audioSource.clip = clipToPlay.track;
        audioSource.Play();
    }

    public void ChangeTrackForState(string state)
    {
        StartCoroutine(FadeOutAndPlayNextTrack(state));
    }

    public IEnumerator FadeOutAndPlayNextTrack(string state)
    {
        float currentTIme = 0;
        float start = audioSource.volume;
        while (currentTIme < 2f)
        {
            currentTIme += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 0, currentTIme / 2f);
            if(audioSource.volume == 0)
            {
                audioSource.Stop();
                ChangeTrackWithoutFade(state);
                audioSource.volume = start;
                //audioSource.Play();
            }
            yield return null;
        }
        yield break;
    }
}
