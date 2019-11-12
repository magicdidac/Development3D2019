using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static bool IsForwarNearToZero(float f)
    {
        return (f < .01f && f > -.01f);
    }
}
