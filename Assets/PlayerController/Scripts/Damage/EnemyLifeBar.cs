using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar : MonoBehaviour
{

    [SerializeField] private Image lifeBar = null;
    [SerializeField] private Image lifeBarWhite = null;
    [SerializeField] private Vector3 offset = Vector2.zero;
    [HideInInspector] public Damager dmgr;
    [HideInInspector] private Transform player;

    [HideInInspector] private float maxScale = .8f;

    private void Start()
    {
        player = GameManager.instance.player.transform;
        transform.localScale = Vector3.zero;

        lifeBar.fillAmount = (float)dmgr.health / dmgr.maxHealth;
        lifeBarWhite.fillAmount = lifeBar.fillAmount;

    }

    public void Update()
    {
        try
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(dmgr.transform.position + offset);
            transform.position = screenPosition;
            transform.localScale = Vector3.one * (((.3f-maxScale)/15) * Vector3.Distance(dmgr.transform.position, player.position) + maxScale);
        }
        catch { Destroy(gameObject); }
        
        lifeBar.fillAmount = (float)dmgr.health / dmgr.maxHealth;
        lifeBarWhite.fillAmount = (Vector2.MoveTowards(new Vector2(lifeBarWhite.fillAmount, 0), new Vector2(lifeBar.fillAmount, 0), .5f * Time.deltaTime)).x;
    }

}
