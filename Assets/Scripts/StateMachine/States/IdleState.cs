using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private Animator anim;

    public IdleState(Animator anim)
    {
        this.anim = anim;
    }

    public void Enter()
    {
        anim.SetFloat("Speed", 0);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}
