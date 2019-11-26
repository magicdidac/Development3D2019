using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState
{
    private Animator anim;
    private PlayerController player;

    public FallState(Animator anim, PlayerController player)
    {
        this.anim = anim;
        this.player = player;
    }

    public void Enter()
    {
        anim.SetBool("isGrounded", false);
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        anim.SetBool("isGrounded", true);
    }
}
