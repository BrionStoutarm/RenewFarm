using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public Placeable m_objectToPlace = null;
    private bool m_placeMode = false;
    private GameObject m_preview = null;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_placeMode && m_objectToPlace)
        {
            //Get mouse point
            Vector3 placeLoc = GetPlaceLoc();

            //Show preview
            ShowPreview(placeLoc);

            //Place object
            //TODO:  make sure placeLoc is in playable bounds
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(m_objectToPlace).transform.position = placeLoc;
            }
        }
    }

    private void ShowPreview(Vector3 placeLoc)
    {
        if (m_preview)
        {
            m_preview.transform.position = placeLoc;
            if(placeLoc.z < 0)
                Debug.Log("Position: " + placeLoc);
        }
        else
        {
            Debug.Log("Should only see this once");
            m_preview =  Instantiate(m_objectToPlace.gameObject);
            m_preview.layer = LayerMask.NameToLayer("Ignore Raycast");
            //var color = m_preview.gameObject.GetComponent<Renderer>().material.color;
            //var newColor = new Color(color.r, color.g, color.b, 0.5f);
            //m_preview.gameObject.GetComponent<Renderer>().material.color = newColor;
            //Instantiate(m_preview, placeLoc, Quaternion.identity);
        }
    }

    public void SetObjectToPlace(Placeable obj)
    {
        m_objectToPlace = null;
        m_objectToPlace = obj;
    }

    public bool IsPlacing() { return m_placeMode; }
    public void SetPlaceMode(bool val) { m_placeMode = val; }

    Vector3 GetPlaceLoc()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Vector3 placePos = new Vector3(0, 0, 0);

        if (Physics.Raycast(ray, out hit))
        {
            //Find side of cube raycast hits
            placePos = hit.collider.gameObject.transform.position;
            int hitSide = FindSideHit(hit);

            switch (hitSide)
            {
                case 1:                 //top
                    placePos.y += 1;
                    break;
                case -1:                //bottom
                    placePos.y -= 1;
                    break;
                case 2:                 //right
                    placePos.x += 1;
                    break;
                case 3:                 //front
                    placePos.z -= 1;
                    break;
                case 4:                 //left
                    placePos.x -= 1;
                    break;
                case 5:                 //back
                    placePos.z += 1;
                    break;
            }
        }
        else
        {
            placePos = Input.mousePosition + new Vector3(10, -10, 0);
        }
        if (placePos.z < 0)
            Debug.Log("Here");
        return placePos;
    }

    int FindSideHit(RaycastHit hit)
    {
        Transform cubeTransform = hit.collider.gameObject.transform;

        //Debug.DrawLine(cubeTransform.position, hit.point, Color.red, 10f);

        float dot = Vector3.Dot(cubeTransform.up, hit.normal);

        float angle = Vector3.SignedAngle(hit.normal, transform.right, Vector3.up);

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
}
