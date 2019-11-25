using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{

    [SerializeField] private int initialLifes = 8;
    [SerializeField] private int initialLives = 3;

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

        GameManager.instance.uiController.Refresh();

    }

    public void IncreaseLifes()
    {
        currentLifes++;

        GameManager.instance.uiController.Refresh();

    }

    private void Die()
    {
        currentLifes = 0;

        GameManager.instance.uiController.Refresh();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Star")
        {
            IncreaseLifes();

            // TODO: Sound

            Destroy(other.gameObject);
        }

        if(other.tag == "Kill")
        {
            Die();
        }

    }


}
