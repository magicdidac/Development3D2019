using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DebugUI : MonoBehaviour
{ 
    [SerializeField] private TMP_Text stateText = null;
    [SerializeField] private TMP_Text statatsText = null;

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

        statatsText.text = "isGrounded: " + player.isGrounded +
            "\nrecentJump: " + player.recentJump +
            "\ncanWallJump: " + player.CanWallJump();

    }

}
