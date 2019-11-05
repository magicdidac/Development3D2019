using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : Damager
{

    [HideInInspector] private bool isDie = true;
    [SerializeField] private Animation anim = null;
    [HideInInspector] public TMP_Text text = null;

    public override void GetDamage(int amount)
    {

        health = 0;

        if (!isDie)
        {
            text.text = (int.Parse(text.text) + 100).ToString();
            isDie = true;
            anim.CrossFade("GoDownAnim", 0);
        }
    }

    public void RestartTarget()
    {
        anim.CrossFade("GoUpAnim", 0);
        isDie = false;
        health = 1;
    }

    public void ResetTarget()
    {
        if (!isDie)
            anim.CrossFade("GoDownAnim", 0);

        isDie = true;
        health = 0;
    }

}
