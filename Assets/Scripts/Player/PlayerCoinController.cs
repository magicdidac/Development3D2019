using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinController : MonoBehaviour
{
    [HideInInspector] public int coins { get; private set; }
    [SerializeField] private GameObject coinParticles = null;

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
            GameManager.instance.audioManager.Play("Sound-Coin");
            Destroy(Instantiate(coinParticles, other.transform.position, Quaternion.identity), 1f);
            Destroy(other.gameObject);
        }
    }

}
