using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggerEvents : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Attack()
    {
        player.attackFX.Play();       
    }
}
