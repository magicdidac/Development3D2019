using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveTargetSpawner : MonoBehaviour
{

    [HideInInspector] public TMP_Text text = null;
    [SerializeField] private GameObject targetPrefab = null;

    public void SpawnTarget()
    {
        Instantiate(targetPrefab, transform.position, Quaternion.identity).GetComponent<MoveTarget>().text = text;
    }

}
