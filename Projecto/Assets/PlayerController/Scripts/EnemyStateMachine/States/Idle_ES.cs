using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_ES : AState
{
    public Idle_ES(EnemyStateMachine self) : base(self)
    {

    }

    protected  override void DoStart()
    {
    }

    public override void DoUpdate()
    {
        
    }

    public override void DoFixedUpdate()
    {
        
    }

    public override void DoExit()
    {
        
    }

    public override AState ChangeState()
    {
        if (self.damager.health <= 0)
            return new Die_ES(self);

        if (Time.time - startTime > 1)
            return new Patrol_ES(self);

        if (self.recievedCritical)
            return new Hit_ES(self, this);

        return null;
    }

}
