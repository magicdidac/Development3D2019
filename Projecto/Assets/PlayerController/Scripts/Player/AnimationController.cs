using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{

    [HideInInspector] private Animation anim;


    public AnimationController(Animation anim)
    {
        this.anim = anim;
    }

    public void StartAnimation(string animation, bool exitTime)
    {

        if (!this.anim.isActiveAndEnabled)
            return;

        if (!exitTime || (exitTime && !this.anim.isPlaying))
            this.anim.Play(animation);

    }

}
