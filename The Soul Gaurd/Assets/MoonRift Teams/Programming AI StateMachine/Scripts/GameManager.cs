using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MusicManager musicManager;

    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    private void Start()
    {
        musicManager.ChangeTrackWithoutFade("Default");
    }
}
