using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private Animator anim;
    private PlayerController player;

    public RunState(Animator anim, PlayerController player)
    {
        this.anim = anim;
        this.player = player;
    }

    public void Enter()
    {
        player.speed = player.runSpeed;
        anim.SetFloat("Speed", 1);
    }

    public void Execute()
    {
    }

    public void Exit()
    {

    }
}
