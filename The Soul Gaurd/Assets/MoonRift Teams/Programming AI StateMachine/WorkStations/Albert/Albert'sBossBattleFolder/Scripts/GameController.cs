using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public GameObject canvas, winner, healthBar, sight, miniMap, restartButton;
    public BossHealth bossHealth;
    public ChaseScript bossChase;
    public BossAIMovement bossAI;
    public AgentController player;
    public Stat playerHealth;
    public Text winOrLose;
    public Image bloodBGround;
    // Start is called before the first frame update
    void Start()
    {
        //canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth.curHealth <= 0)
        {
            winner.SetActive(true);
            healthBar.SetActive(false);
            sight.SetActive(false);
            miniMap.SetActive(false);
        }

        if(playerHealth.MyCurrentValue <= 0)
        {
            winner.SetActive(true);
            restartButton.SetActive(true);
            healthBar.SetActive(false);
            sight.SetActive(false);
            miniMap.SetActive(false);
            winOrLose.text = "You Lost";
            winOrLose.color = Color.red;
            bossChase.anim.SetTrigger("Eat");
            bossAI.anim.SetTrigger("Eat");
            bossAI.minDist = 1.5f;
            bossAI.followPlayer = false;
            
        }

        if (player.hurt)
        {
            bloodBGround.enabled = true;
        }
    }
}
