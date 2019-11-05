using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_ES : AState
{

    public Chase_ES(EnemyStateMachine self) : base(self)
    {
        self.agent.isStopped = false;
    }

    protected  override void DoStart()
    {
    }

    public override void DoUpdate()
    {
        self.agent.SetDestination(self.player.position);
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

        if (self.recievedCritical)
            return new Hit_ES(self, this);

        if (Vector3.Distance(self.transform.position, self.player.position) > self.maxChaseDistance)
            return new Patrol_ES(self);

        if (Vector3.Distance(self.transform.position, self.player.position) <= self.minChaseDistance)
            return new Attack_ES(self);

        if (!self.directCastPlayer)
            return new Patrol_ES(self);

        return null;
    }

}
