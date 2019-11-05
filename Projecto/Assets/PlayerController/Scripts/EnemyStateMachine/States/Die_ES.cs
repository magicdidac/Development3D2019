using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_ES : AState
{
    public Die_ES(EnemyStateMachine self) : base(self)
    {
        self.dissolveModel.Dissappear();
        self.damager.isDead = true;
    }

    protected  override void DoStart()
    {
        
    }

    public override void DoUpdate()
    {
        if (self.dissolveModel.IsDisappeared())
            self.damager.Die();
    }

    public override void DoFixedUpdate()
    {
    }

    public override void DoExit()
    {
        
    }

    public override AState ChangeState()
    {
        return null;
    }

}
