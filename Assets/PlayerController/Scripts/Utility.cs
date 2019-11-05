using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{

    public static bool CompareVectors3(Vector3 v1, Vector3 v2)
    {
        v1 = RoundVector3(v1);
        v2 = RoundVector3(v2);

        return v1 == v2;

    }

    public static Vector3 RoundVector3(Vector3 v)
    {
        return new Vector3((float)Math.Round(v.x, 2), (float)Math.Round(v.y, 2), (float)Math.Round(v.z, 2));
    }

}
