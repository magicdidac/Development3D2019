using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchState : IState
{

    private Animator anim;

    private PunchBehaviour.TPunchType punchType;

    public PunchState(Animator anim, PunchBehaviour.TPunchType punchType)
    {
        this.anim = anim;
        this.punchType = punchType;
    }

    public void Enter()
    {
        switch (punchType)
        {
            case PunchBehaviour.TPunchType.RIGHT_HAND:
                anim.SetTrigger("PunchRight");
                break;
            case PunchBehaviour.TPunchType.LEFT_HAND:
                anim.SetTrigger("PunchLeft");
                break;
            case PunchBehaviour.TPunchType.FOOT:
                anim.SetTrigger("Kick");
                break;
        }
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}
