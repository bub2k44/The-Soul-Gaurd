using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string targetScene;
    private LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }
    private void OnTriggerEnter(Collider other)
    {
        MyPlayerInput player = other.GetComponent<MyPlayerInput>();
        if(player)
        {
            levelLoader.LoadTargetLevel(targetScene);
        }
    }
}
