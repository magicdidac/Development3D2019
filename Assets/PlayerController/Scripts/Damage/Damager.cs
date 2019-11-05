using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{

    [SerializeField] public int maxHealth = 100;
    [HideInInspector] public int health;
    [HideInInspector] protected GameManager gm;

    

    protected void Start()
    {
        health = maxHealth;

        gm = GameManager.instance;
    }

    public virtual void GetDamage(int amount)
    {
        health -= amount;
    }

    public void AddHealth(int ammount)
    {
        health += ammount;
        if (health > maxHealth)
            health = maxHealth;

    }

    public bool HaveMaxHealth()
    {
        return health >= maxHealth;
    }

    


}
