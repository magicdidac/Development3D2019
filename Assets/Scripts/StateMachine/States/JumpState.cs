using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    private Animator anim;
    private PlayerController player;
    private int jump;

    public JumpState(Animator anim, PlayerController player, int jump)
    {
        this.anim = anim;
        this.player = player;
        this.jump = jump;
    }

    public void Enter()
    {
        switch (jump)
        {
            case 0:
                anim.SetTrigger("Jump");
                player.verticalSpeed = player.jumpForce;
                break;
            case 1:
                anim.SetTrigger("DoubleJump");
                player.verticalSpeed = player.doubleJumpForce;
                break;
            case 2:
                anim.SetTrigger("TripleJump");
                player.verticalSpeed = player.tripleJumpForce;
                break;

        }

        player.recentJump = true;

    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}
