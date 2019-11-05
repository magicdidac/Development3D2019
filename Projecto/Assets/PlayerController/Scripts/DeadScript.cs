using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScript : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.instance.uiController.Revive();
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
