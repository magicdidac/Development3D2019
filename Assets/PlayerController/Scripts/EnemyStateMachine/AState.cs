using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState
{

    protected float startTime = 0;
    protected GameManager gm;
    protected EnemyStateMachine self;

    public AState(EnemyStateMachine self)
    {
        this.startTime = Time.time;

        gm = GameManager.instance;

        this.self = self;

        DoStart();
    }

    protected abstract void DoStart();

    public abstract void DoUpdate();

    public abstract void DoFixedUpdate();

    public abstract void DoExit();

    public abstract AState ChangeState();

    public override string ToString()
    {
        return this.GetType()+"";
    }

}
