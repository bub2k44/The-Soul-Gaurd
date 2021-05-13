using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour 
{
    public int curHealth = 0;
    public int maxHealth = 100;

    float last_dmg;
    bool hurt;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {

        curHealth = maxHealth;

        last_dmg = 0;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
     public void takeDmg(int incoming_dmg){
        float elapsed_time = Time.time - last_dmg;

        if(elapsed_time > 1){
            curHealth = Mathf.Max(0, curHealth - incoming_dmg);
            healthBar.SetHealth(curHealth);
        }
    }

    public void Life(int life)
    {
        curHealth -= life;

        healthBar.SetHealth(curHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DHitBox"))
        {
             hurt = true;
            

        }
    }
}
