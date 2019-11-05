using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveTarget : Damager
{

    [SerializeField] private GameObject plate = null;
    [SerializeField] private ParticleSystem particles = null;
    [SerializeField] private float speed = 5;

    [HideInInspector] public TMP_Text text = null;
    [HideInInspector] private Vector3 maxPos = Vector3.zero;

    public override void GetDamage(int amount)
    {
        if(health > 0)
        {
            health = 0;
            particles.Play();
            text.text = (int.Parse(text.text) + 100).ToString();
            Destroy(plate);
            Destroy(gameObject, 2);
        }
    }

    private void FixedUpdate()
    {
        if (maxPos == Vector3.zero) {
            maxPos = transform.position + (Vector3.up * 15);
        }

        transform.position = Vector3.MoveTowards(transform.position, maxPos, speed * Time.deltaTime);

        if(transform.position == maxPos)
            Destroy(gameObject);
    }

}
