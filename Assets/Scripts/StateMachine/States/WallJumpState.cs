using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : IState
{
    private Animator anim;
    private PlayerController player;

    public WallJumpState(Animator anim, PlayerController player)
    {
        this.anim = anim;
        this.player = player;
    }

    public void Enter()
    {
        anim.SetTrigger("WallJump");
        player.needsWallJump = true;
        player.verticalSpeed = player.jumpForce;
        player.Invoke("ResetWallJump",.5f);
        player.wallForward = -player.lastForward;
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        
    }
}
