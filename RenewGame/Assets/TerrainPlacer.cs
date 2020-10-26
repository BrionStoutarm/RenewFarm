using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPlacer : MonoBehaviour
{
    private PlacementGrid m_grid;
    public GameObject groundSeg;

    private void Awake()
    {
        m_grid = FindObjectOfType<PlacementGrid>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TerrainManipToggle.isTerrainPlace())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Debug.DrawLine(ray.GetPoint(100.0f), Camera.main.transform.position, Color.red, 10.0f);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    PlaceTerrainNear(hit.point);
                }
            }

        }
    }

    private void PlaceTerrainNear(Vector3 point)
    {
        var finalPosition = m_grid.GetNearestPointOnGrid(point);
        Instantiate(groundSeg).transform.position = finalPosition;
    }
}
