using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert_ES : AState
{
    private bool isEnded = false;

    public Alert_ES(EnemyStateMachine self) : base(self)
    {
        self.agent.isStopped = true;
        self.agent.ResetPath();
        self.StartCoroutine(Rotate(2));

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
        isEnded = true;
    }

    IEnumerator Rotate(float duration)
    {
        float startRotation = self.transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            if (isEnded)
                break;

            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            self.transform.eulerAngles = new Vector3(self.transform.eulerAngles.x, yRotation, self.transform.eulerAngles.z);
            yield return null;
        }
        isEnded = true;
    }

    public override AState ChangeState()
    {
        if (self.damager.health <= 0)
            return new Die_ES(self);

        if (self.recievedCritical)
            return new Hit_ES(self, this);

        if(isEnded && !self.seePlayer)
            return new Patrol_ES(self);

        if (self.seePlayer)
            return new Chase_ES(self);

        return null;

    }
}
