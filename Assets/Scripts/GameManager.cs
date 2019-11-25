using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    [HideInInspector] public PlayerController player;
    [HideInInspector] public UIController uiController;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void Revive()
    {

    }

}
