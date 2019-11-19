using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : Door
{

    protected override void Start()
    {
        base.Start();

        anim.SetBool("Open", false);
    }

    protected override void Update() { }
    
    public override void InteractNegative() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            InteractPositive();
        }
    }

}
