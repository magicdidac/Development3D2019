using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    [HideInInspector] public PlayerController player;
    [HideInInspector] public UIController uiController;
    [HideInInspector] public AudioManager audioManager;
    [HideInInspector] public Transform savedPlayerPos;
    [HideInInspector] public InputMaster controls;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);


        controls = new InputMaster();

        controls.Enable();

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void Revive()
    {
        player.Revive(savedPlayerPos);
    }

}
