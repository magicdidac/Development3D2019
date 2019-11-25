using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinController : MonoBehaviour
{
    [HideInInspector] public int coins { get; private set; }

    public void IncreaseCoins()
    {
        coins++;
        GameManager.instance.uiController.Refresh();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            IncreaseCoins();
            // TODO: Sound
            Destroy(other.gameObject);
        }
    }

}
