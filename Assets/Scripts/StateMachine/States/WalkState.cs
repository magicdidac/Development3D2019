using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    private Animator anim;
    private PlayerController player;

    public WalkState(Animator anim, PlayerController player)
    {
        this.anim = anim;
        this.player = player;
    }

    public void Enter()
    {
        player.speed = player.walkSpeed;
        anim.SetFloat("Speed", .2f);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        
    }

}
