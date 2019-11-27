using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeState : IState
{
    private Animator anim;

    public TakeState(Animator anim)
    {
        this.anim = anim;
    }

    public void Enter()
    {
        anim.SetBool("Shell", true);
    }

    public void Execute()
    {

    }

    public void Exit()
    {
    }

}