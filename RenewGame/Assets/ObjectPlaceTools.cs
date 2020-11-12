using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ObjectPlaceTools : MonoBehaviour
{
    static bool m_destroy = false;
    static bool m_place = false;
    static bool showTerrainMenu = false;
    public Placeable m_basicGroundSeg;
    public Placeable m_basicRamp;
    public Placeable m_basicPath;
    public ObjectPlacer m_placer;


    public void TogglePathwayPlace()
    {
        Debug.Log("Pathway Placement");
        m_place = true;
        m_placer.ClearPlacer();
        m_placer.SetObjectToPlace(m_basicPath);
    }

    public void ToggleTerrainDestroy ()
    {
        m_destroy = !m_destroy;
        if (m_destroy) 
        {
            m_place = false;
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
            m_destroy = false;
        }
        else
        {
            m_placer.SetObjectToPlace(null);
        }

        Debug.Log("Turning Terrain Place to: " + m_place.ToString());
    }

    public void ToggleRampPlace()
    {
        m_place = !m_place;
        if (m_place)
        {
            m_placer.SetObjectToPlace(m_basicRamp);
            m_destroy = false;
        }
        else
        {
            m_placer.SetObjectToPlace(null);
        }
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
