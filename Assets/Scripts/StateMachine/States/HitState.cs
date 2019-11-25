using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : IState
{
    private Animator anim;
    private PlayerLifeController lifeController;

    public HitState(Animator anim, PlayerLifeController lifeController)
    {
        this.anim = anim;
        this.lifeController = lifeController;
    }

    public void Enter()
    {
        anim.SetTrigger("Hit");
        lifeController.DecreaseLifes();
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
