using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, BasicStats basicStats, string animBoolName) : base(player, stateMachine, basicStats, animBoolName)
    {
    }
}
