using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameC : MonoBehaviour
{
   
    TeamTwo[] twoCombatants;
    TeamOne[] oneCombatants;
    public GameObject text;
    public GameObject resultText;
    public Text winLose, numOfGoodtext, numOfBadText;
    public float timeStart;
    // Start is called before the first frame update
    void Start()
    {
        oneCombatants = GameObject.FindObjectsOfType<TeamOne>();
        twoCombatants = GameObject.FindObjectsOfType<TeamTwo>();

        text.SetActive(true);
        numOfGoodtext.enabled = true;
        numOfBadText.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        timeStart += Time.deltaTime;
        twoCombatants = GameObject.FindObjectsOfType<TeamTwo>();
        int twoCOunt = twoCombatants.Length; 
        oneCombatants = GameObject.FindObjectsOfType<TeamOne>();
        int oneCOunt = oneCombatants.Length;

        numOfGoodtext.text = oneCOunt.ToString();
        numOfBadText.text = twoCOunt.ToString();
        if (timeStart > 2)
        {
            text.SetActive(false);
            
        }

        

        if (oneCOunt <= 0)
        {
            resultText.SetActive(true);

            winLose.text = "You Lose";
        }
        if(twoCOunt <= 0)
        {
            resultText.SetActive(true);

            winLose.text = "You Win";
        }
    }
}
