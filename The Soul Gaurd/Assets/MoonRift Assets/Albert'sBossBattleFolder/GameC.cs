using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameC : MonoBehaviour
{
    public int teamOneCount;
    public int teamTwoCount;

    public GameObject text;
    public float timeStart;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("TeamOne", teamOneCount);
        PlayerPrefs.SetInt("TeamTwo", teamTwoCount);
        text.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timeStart += Time.deltaTime;

        teamOneCount = PlayerPrefs.GetInt("TeamOne");
        teamTwoCount = PlayerPrefs.GetInt("TeamTwo");

        if(timeStart > 2)
        {
            text.SetActive(false);
            
        }

        if(teamOneCount <= 0)
        {
            Debug.Log("Team One Lost");
        }
        if(teamTwoCount <= 0)
        {
            Debug.Log("team Two lost");
        }
    }
}
