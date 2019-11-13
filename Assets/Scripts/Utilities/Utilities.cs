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

public static class Variables
{
    public static Color blueColor = new Color(73f/255f, 151f/255f, 200f/255f);
    public static Color orangeColor = new Color(188f/255f, 133f/255f, 56f/255f);

}
