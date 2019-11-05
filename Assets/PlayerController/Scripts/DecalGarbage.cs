using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DecalGarbage
{

    private static Queue<GameObject> decals = new Queue<GameObject>();
    private static int maxDecals = 25;

    public static void AddDecal(GameObject decal)
    {
        decals.Enqueue(decal);

        if (decals.Count > maxDecals)
            GameObject.Destroy(decals.Dequeue());

    }

}
