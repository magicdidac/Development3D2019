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

        if(currentLifes <= 0)
        {
            currentLives--;
            currentLifes = initialLifes;
        }

        if(currentLives <= 0)
        {
            Application.Quit();
        }

    }

}
