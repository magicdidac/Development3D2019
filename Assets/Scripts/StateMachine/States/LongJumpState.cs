using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongJumpState : IState
{
    private Animator anim;
    private PlayerController player;

    public LongJumpState(Animator anim, PlayerController player)
    {
        this.anim = anim;
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("LongJump");
        anim.SetTrigger("LongJump");
        player.needsLongJump = true;
        player.verticalSpeed = player.jumpForce;
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        
    }

}