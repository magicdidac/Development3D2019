using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{

    [SerializeField] private int initialLifes = 8;
    [SerializeField] private int initialLives = 3;

    [SerializeField] private GameObject starParticles = null;
    [SerializeField] private GameObject deadParticles = null;

    [HideInInspector] public int currentLifes { get; private set; }
    [HideInInspector] public int currentLives { get; private set; }

    private void Start()
    {
        currentLifes = initialLifes;
        currentLives = initialLives;
    }

    public void DecreaseLifes()
    {
        currentLifes--;

        if (currentLifes < 0)
            currentLifes = 0;

        if (currentLifes <= 0)
            Die();

        GameManager.instance.uiController.Refresh();

    }

    public void IncreaseLifes()
    {
        currentLifes++;

        if (currentLifes > initialLifes)
            currentLifes = initialLifes;

        GameManager.instance.uiController.Refresh();

    }

    private void Die()
    {
        currentLifes = 0;

        Destroy(Instantiate(deadParticles, transform.position, Quaternion.identity), 1f);

        GameManager.instance.uiController.Refresh();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Star")
        {
            IncreaseLifes();

            GameManager.instance.audioManager.Play("Sound-Star");

            Destroy(Instantiate(starParticles, other.transform.position, Quaternion.identity), 1f);

            Destroy(other.gameObject);
        }

        if(other.tag == "Kill")
        {
            Die();
        }

    }

    public void Revive()
    {
        currentLives--;
        currentLifes = initialLifes;
    }


}
