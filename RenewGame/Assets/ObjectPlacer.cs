using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacer : MonoBehaviour
{
    public float m_rotateSpeed = 10000f;
    public Placeable m_objectToPlace = null;
    private GameObject m_preview = null;
    private bool m_destroy = false;

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
        if(Input.GetMouseButtonDown(1))
        {
            ClearPlacer();
        }
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
        else if(m_destroy)
        {
            GameObject toDestroy = GetObjectAtMouse();
            if(toDestroy && Input.GetMouseButtonDown(0))
            {
                Placeable placeObj = toDestroy.GetComponent<Placeable>();
                if(placeObj)
                {
                    placeObj.DestroyThis();
                }
            }
        }
    }

    private GameObject GetObjectAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject obj = hit.transform.gameObject;
            if (obj.transform.parent != null)
                obj = obj.transform.parent.gameObject;

            if (obj.CompareTag("Placeable"))
            {
                //Debug.Log("Hit Placeable");
                return obj;
            }
        }

        return null;
    }
    public void SetDestroy(bool val)
    {
        Debug.Log("Setting placer destroy to: " + val.ToString());
        m_destroy = val;
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
            ApplyIgnoreRaycastLayer(m_preview.transform);
        }
    }

    private void ApplyIgnoreRaycastLayer(Transform root)
    {
        Stack<Transform> moveTargets = new Stack<Transform>();
        moveTargets.Push(root);
        Transform currentTarget;
        while (moveTargets.Count != 0)
        {
            currentTarget = moveTargets.Pop();
            currentTarget.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            foreach (Transform child in currentTarget)
                moveTargets.Push(child);
        }
    }

    public void ClearPreview()
    {
        m_preview = null;
    }

    public void ClearPlacer()
    {
        if (m_preview != null)
            Destroy(m_preview);

        if (m_objectToPlace)
            m_objectToPlace.CancelPlacement();

        m_objectToPlace = null;
        m_preview = null;
        m_destroy = false;
    }

    public void SetObjectToPlace(Placeable obj)
    {
        if (m_objectToPlace)
            m_objectToPlace.CancelPlacement();

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
