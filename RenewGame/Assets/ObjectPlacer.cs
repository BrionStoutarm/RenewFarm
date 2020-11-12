using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacer : MonoBehaviour
{
    public float m_rotateSpeed = 10000f;
    public Placeable m_objectToPlace = null;
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
        if (m_objectToPlace)
        {
            m_objectToPlace.IsPlacing(true);

            //Get mouse point
            Vector3 placeLoc = GetPlaceLoc();

            //Show preview
            ShowPreview(placeLoc);

            //Place object
            //TODO:  make sure placeLoc is in playable bounds

            if (Input.GetMouseButtonDown(0))
            {
                //Instantiate(m_objectToPlace).transform.position = placeLoc;
                m_objectToPlace.Place(placeLoc);
            }
        }
    }

    private void ShowPreview(Vector3 placeLoc)
    {
        if (m_preview)
        {
            m_preview.transform.position = placeLoc;
        }
        else
        {
            Debug.Log("Should only see this once");
            m_preview = m_objectToPlace.GetPreview();
            //m_preview.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }

    public void ClearPreview()
    {
        m_preview = null;
    }

    public void ClearPlacer()
    {
        m_objectToPlace = null;
        m_preview = null;
    }

    public void SetObjectToPlace(Placeable obj)
    {
        m_objectToPlace = null;
        m_preview = null;
        m_objectToPlace = obj;
    }

    Vector3 GetPlaceLoc()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Vector3 placePos = new Vector3(0, 0, 0);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 offset = m_objectToPlace.transform.localScale;
            placePos = hit.point;
            placePos.y = placePos.y + (offset.y / 2);
        }

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
