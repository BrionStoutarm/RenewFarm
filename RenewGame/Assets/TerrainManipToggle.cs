using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManipToggle : MonoBehaviour
{
    static bool m_destroy = false;
    static bool m_place = false;
    static bool showTerrainMenu = false;

    public void ToggleTerrainManip ()
    {
        m_destroy = !m_destroy;
        if (m_destroy)
            m_place = false;
        Debug.Log("Turning Terrain Destroy to: " + m_place.ToString());
    }

    public static bool isDestroy()
    {
        return m_destroy;
    }

    public void ToggleTerrainPlace()
    {
        m_place = !m_place;
        if (m_place)
            m_destroy = false;
        Debug.Log("Turning Terrain Place to: " + m_place.ToString());
    }

    public static bool isTerrainPlace()
    {
        return m_place;
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
