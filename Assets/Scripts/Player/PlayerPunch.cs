using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{

    [SerializeField] private PlayerController player = null;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        
        if(other.GetComponent<Goomba>() && player.punchIsActive)
        {
            Debug.Log("YAS");
            other.GetComponent<Goomba>().Die();
        }
    }
}
