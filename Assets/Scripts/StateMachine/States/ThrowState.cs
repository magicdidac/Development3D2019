using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : IState
{
    private Animator anim;
    private PlayerController player;

    public ThrowState(Animator anim, PlayerController player)
    {
        this.anim = anim;
        this.player = player;
    }

    public void Enter()
    {
        anim.SetBool("Shell", false);
        player.shell.Throw();
        player.shell = null;
    }

    public void Execute()
    {

    }

    public void Exit()
    {
    }

}
