using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingGallery : MonoBehaviour
{

    [SerializeField] private Target[] targets = null;
    [SerializeField] private MoveTargetSpawner[] moveTargetSpawners = null;
    [SerializeField] private TMP_Text text = null;
    [SerializeField] private GalleryButton button = null;
    [SerializeField] private Animation anim = null;

    private void Start()
    {
        foreach(Target t in targets)
        {
            t.text = text;
        }

        foreach(MoveTargetSpawner t in moveTargetSpawners)
        {
            t.text = text;
        }
    }

    public void ResetTargets()
    {
        foreach(Target t in targets)
        {
            t.ResetTarget();
        }
    }

    public void RestartTarget(int i)
    {
        targets[i].RestartTarget();
    }

    public void SpawnMoveTarget(int i)
    {
        moveTargetSpawners[i].SpawnTarget();
    }

    public void SetButonOn()
    {
        button.canInteract = true;
    }

    public void GalleryStart()
    {
        text.text = "0";
        anim.CrossFade("Shooting", 0);
    }

}
