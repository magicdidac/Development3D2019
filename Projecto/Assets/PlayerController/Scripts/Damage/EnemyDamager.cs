using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamager : Damager
{

    [Header("Prefabs")]
    [SerializeField] private GameObject ammoPickable = null;
    [SerializeField] private GameObject healthPickable = null;
    [SerializeField] private GameObject shieldPickable = null;
    [SerializeField] private GameObject deadParticles = null;
    [SerializeField] private MeshRenderer render = null;

    [HideInInspector] private EnemyLifeBar lifeBar = null;
    [HideInInspector] public EnemyStateMachine self;

    [HideInInspector] public bool isDead = false;

    public override void GetDamage(int amount)
    {
        health -= amount;
        if (amount != 0)
            self.recievedCritical = true;
        else
            self.recievedDamage = true;
    }

    public void Die()
    {
        Destroy(Instantiate(deadParticles, render.transform.position, Quaternion.identity), .5f);
        gm.audioManager.PlaySound("DroneExplosion");

        SpawnPickable(-1, 3, ammoPickable);
        SpawnPickable(1, 2, healthPickable);
        SpawnPickable(-5, 2, shieldPickable);

        Destroy(gameObject);
    }

    private void SpawnPickable(int minPickable, int maxPickables, GameObject pickable)
    {
        int rngNumber = Random.Range(minPickable, maxPickables + 1);

        for (int i = 0; i < rngNumber; i++)
        {
            Instantiate(pickable, render.transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (lifeBar != null && isDead)
        {
            Destroy(lifeBar.gameObject);
            lifeBar = null;
        }

        if (isDead)
            return;

        if (lifeBar == null && self.directCastPlayer && Vector3.Distance(transform.position, gm.player.transform.position) < 15)
            lifeBar = FloatingTextController.CreateFloatingBar(this, transform.position);
        else if(lifeBar != null && (!self.directCastPlayer || Vector3.Distance(transform.position, gm.player.transform.position) >= 15)){
            Destroy(lifeBar.gameObject);
            lifeBar = null;
        }

    }

}
