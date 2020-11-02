using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManipToggle : MonoBehaviour
{
    static bool terrainDestroyOn = false;
    static bool terrainPlaceOn = false;
    static bool showTerrainMenu = false;

    public void ToggleTerrainManip ()
    {
        terrainDestroyOn = !terrainDestroyOn;
        if (terrainDestroyOn)
            terrainPlaceOn = false;
        Debug.Log("Turning Terrain Destroy to: " + terrainPlaceOn.ToString());
    }

    public static bool isTerrainDestroy()
    {
        return terrainDestroyOn;
    }

    public void ToggleTerrainPlace()
    {
        terrainPlaceOn = !terrainPlaceOn;
        if (terrainPlaceOn)
            terrainDestroyOn = false;
        Debug.Log("Turning Terrain Place to: " + terrainPlaceOn.ToString());
    }

    public static bool isTerrainPlace()
    {
        return terrainPlaceOn;
    }

    public void ToggleTerrainMenu()
    {
        showTerrainMenu = !showTerrainMenu;
    }

    public static bool showTerrainManipMenu()
    {
        return showTerrainMenu;
    }
}
