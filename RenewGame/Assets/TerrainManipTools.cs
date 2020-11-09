using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManipTools : MonoBehaviour
{
    static bool m_destroy = false;
    static bool m_place = false;
    static bool showTerrainMenu = false;
    public Placeable m_basicGroundSeg;
    public ObjectPlacer m_placer;

    public void ToggleTerrainDestroy ()
    {
        m_destroy = !m_destroy;
        if (m_destroy) 
        {
            m_place = false;
            m_placer.SetPlaceMode(false);
            m_placer.SetObjectToPlace(null);
        }
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
        {
            m_placer.SetObjectToPlace(m_basicGroundSeg);
            m_placer.SetPlaceMode(true);
            m_destroy = false;
        }
        else
        {
            m_placer.SetPlaceMode(false);
            m_placer.SetObjectToPlace(null);
        }

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
