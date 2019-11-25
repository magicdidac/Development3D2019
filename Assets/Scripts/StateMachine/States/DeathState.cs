using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{
    private Animator anim;

    public DeathState(Animator anim)
    {
        this.anim = anim;
    }

    public void Enter()
    {
        anim.SetTrigger("Death");
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}
