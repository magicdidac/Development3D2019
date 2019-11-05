using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_ES : AState
{
    public Attack_ES(EnemyStateMachine self) : base(self)
    {
        self.agent.isStopped = true;
        self.StartCoroutine("AttackRoutine");
    }

    protected  override void DoStart()
    {
    }

    public override void DoUpdate()
    {
        
    }

    public override void DoFixedUpdate()
    {
        self.transform.LookAt(self.player.position);
    }

    public override void DoExit()
    {
        self.StopAllCoroutines();
    }

    

    public override AState ChangeState()
    {
        if (self.damager.health <= 0)
            return new Die_ES(self);

        if (self.recievedCritical)
            return new Hit_ES(self, this);

        if (Vector3.Distance(self.transform.position, self.player.position) > self.maxAttackDistance)
            return new Chase_ES(self);

        return null;
    }

}
