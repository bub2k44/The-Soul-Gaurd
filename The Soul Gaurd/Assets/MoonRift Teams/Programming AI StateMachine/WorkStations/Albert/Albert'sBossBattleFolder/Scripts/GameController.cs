using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public GameObject canvas, winner, healthBar, sight, miniMap, restartButton, fightButton, pauseMenu;
    public BossHealth bossHealth;
    public ChaseScript bossChase;
    public BossAIMovement bossAI;
    public ThirdPersonMovement player;
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
            pauseMenu.SetActive(false);
            //restartButton.SetActive(true);
            //checkpointButton.SetActive(true);
        }

        if(playerHealth.MyCurrentValue <= 0)
        {
            winner.SetActive(true);      
            healthBar.SetActive(false);
            sight.SetActive(false);
            miniMap.SetActive(false);
            winOrLose.text = "You Lost";
            winOrLose.color = Color.red;
            if (bossAI != null)
            {
                bossAI.DeathOfPlayer();
                restartButton.SetActive(true);
            }
            
            bossChase.anim.SetTrigger("Eat");
            //bossAI.anim.SetTrigger("Eat");
            bossAI.minDist = 1.5f;
            if (player.arenaReached)
            {
                fightButton.SetActive(true);
            }
            else
            {
                restartButton.SetActive(true);
            }

        }

        if (player.hurt)
        {
            bloodBGround.enabled = true;
            bloodBGround.color = new Color(bloodBGround.color.r, bloodBGround.color.g, bloodBGround.color.b, 1);

            StartCoroutine(BackGround());
            player.hurt = false;

        }

        
        if (Input.GetKeyDown(KeyCode.Q))// activates pause menu
        {
            player.enabled = false;
            if(bossChase != null)
            {
                bossChase.nav.isStopped = true;
                bossChase.enabled = false;
            }
            if(bossAI != null)
            {
                bossAI.enabled = false;
            }
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        }

        
    }

    public void Resume()
    {
        player.enabled = true;
        if(bossChase != null)
        {
            bossChase.nav.isStopped = false;

            bossChase.enabled = true;
        }
        if (bossAI != null)
        {
            bossAI.enabled = true;
        }
        pauseMenu.SetActive(false);
    }

   

    IEnumerator BackGround()
    {
       
            yield return new WaitForSeconds(.5f);
            bloodBGround.color = new Color(bloodBGround.color.r, bloodBGround.color.g, bloodBGround.color.b, .75f);
            yield return new WaitForSeconds(.4f);
            bloodBGround.color = new Color(bloodBGround.color.r, bloodBGround.color.g, bloodBGround.color.b, .5f);
            yield return new WaitForSeconds(.3f);
            bloodBGround.color = new Color(bloodBGround.color.r, bloodBGround.color.g, bloodBGround.color.b, .25f);

            yield return new WaitForSeconds(.1f);
            bloodBGround.enabled = false;
        
        

    }
}
