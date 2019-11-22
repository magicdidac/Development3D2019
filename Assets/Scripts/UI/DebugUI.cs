using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DebugUI : MonoBehaviour
{ 
    [SerializeField] private TMP_Text stateText = null;

    [HideInInspector] private PlayerController player;

    private void Start()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        if (player == null)
            player = GameManager.instance.player;

        stateText.text = player.GetCurrentState();
    }

}
