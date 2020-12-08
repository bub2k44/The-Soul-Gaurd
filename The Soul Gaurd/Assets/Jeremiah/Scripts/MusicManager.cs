using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    //public List<string> music = new List<string>();

    public Dictionary<string, AudioClip> tracks = new Dictionary<string, AudioClip>();
    
    // Start is called before the first frame update
    void Start()
    {
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
                audioSource.clip = tracks[state];
                audioSource.volume = start;
                audioSource.Play();
            }
            yield return null;
        }
        yield break;
    }
}
