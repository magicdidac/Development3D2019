using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    private static DamagePopup popupText;
    private static EnemyLifeBar popupEnemyLifeBar;
    private static GameObject canvas;
    private static Camera uiCamera;

    public static void Initialize()
    {
        canvas = GameObject.Find("DamagePopups");
        uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        if(!popupText)
            popupText = Resources.Load<DamagePopup>("Prefabs/PopupTextParent");

        if (!popupEnemyLifeBar)
            popupEnemyLifeBar = Resources.Load<EnemyLifeBar>("Prefabs/EnemyLifeBar");
    }

    public static void CreateFloatingText(string text, Vector3 location)
    {
        DamagePopup instance = Instantiate(popupText);
        instance.target = location;

        instance.offset = new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), 0);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location + instance.offset);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);

    }

    public static EnemyLifeBar CreateFloatingBar(EnemyDamager damager, Vector3 location)
    {
        EnemyLifeBar instance = Instantiate(popupEnemyLifeBar);
        instance.dmgr = damager;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;

        return instance;

    }

}
