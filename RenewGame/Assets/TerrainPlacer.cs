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
                    //Find side of cube raycast hits
                    Vector3 placePos = hit.collider.gameObject.transform.position;
                    int hitSide = FindSideHit(hit);
                    
                    switch(hitSide)
                    {
                        case 1:                 //top
                            Debug.Log("Placing on top");
                            placePos.y += 1;
                            break;
                        case -1:                //bottom
                            Debug.Log("Placing on bottom");
                            placePos.y -= 1;
                            break;
                        case 2:                 //right
                            Debug.Log("Placing on right");
                            placePos.x += 1;
                            break;
                        case 3:                 //front
                            Debug.Log("Placing on front");
                            placePos.z -= 1;
                            break;
                        case 4:                 //left
                            Debug.Log("Placing on left");
                            placePos.x -= 1;
                            break;
                        case 5:                 //back
                            Debug.Log("Placing on back");
                            placePos.z += 1;
                            break;
                    }

                    PlaceTerrainAt(placePos);
                }
            }

        }
    }

    int FindSideHit(RaycastHit hit)
    {
        Transform cubeTransform = hit.collider.gameObject.transform;

        Debug.DrawLine(cubeTransform.position, hit.point, Color.red, 10f);

        float dot = Vector3.Dot(cubeTransform.up, hit.normal);
        Debug.Log("Dot = " + dot.ToString());

        float angle = Vector3.SignedAngle(hit.normal, transform.right, Vector3.up);

        Debug.Log("Angle = " + angle.ToString());

        if (dot == 1) //top
            return 1;

        else if (dot == -1) //bottom
            return -1;

        else if (angle == 0) //"right" side
            return 2;

        else if (angle == -90) //"front" side
            return 3;

        else if (angle == 180) //"left" side
            return 4;

        else if (angle == 90) //"back" side
            return 5;


        return 0;
    }

    private void PlaceTerrainAt(Vector3 point)
    {
        Instantiate(groundSeg).transform.position = point;
    }
}
