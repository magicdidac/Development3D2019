using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [HideInInspector] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    [HideInInspector] public int damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(GameManager.instance.player.transform.position);

        rb.velocity = (GameManager.instance.player.transform.position - transform.position) * speed;

        Destroy(gameObject, 1);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPSController>())
        {
            other.GetComponent<FPSController>().dmgShield.GetDamage(damage);
            Destroy(gameObject);
        }
    }

}
