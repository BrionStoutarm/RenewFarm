using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManipToggle : MonoBehaviour
{
    static bool terrainManipOn = false;
    public void ToggleTerrainManip ()
    {
        terrainManipOn = !terrainManipOn;
    }

    public static bool isTerrainManip()
    {
        return terrainManipOn;
    }
}
