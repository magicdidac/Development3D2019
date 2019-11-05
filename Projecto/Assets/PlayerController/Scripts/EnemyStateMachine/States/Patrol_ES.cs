using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_ES : AState
{

    public Patrol_ES(EnemyStateMachine self) : base(self)
    {
        self.agent.isStopped = false;
        self.agent.SetDestination(self.GetNextPosition());
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

        if (self.recievedCritical || self.recievedDamage)
            return new Hit_ES(self, this);

        if (self.listenPlayer)
        {
            self.listenPlayer = false;
            return new Alert_ES(self);
        }

        if (!self.agent.pathPending && self.agent.remainingDistance < .5f)
            return new Idle_ES(self);

        return null;
    }
}
