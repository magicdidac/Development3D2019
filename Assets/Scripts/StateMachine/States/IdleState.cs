using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private Animator anim;
    private PlayerController player;

    public IdleState(Animator anim, PlayerController player)
    {
        this.anim = anim;
        this.player = player;
    }

    public void Enter()
    {
        anim.SetFloat("Speed", 0);
        player.Invoke("DoIdlePlus", 10);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        player.CancelInvoke();
    }

}
