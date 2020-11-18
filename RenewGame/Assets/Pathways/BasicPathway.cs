using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BasicPathway : Placeable
{
    private bool m_isStarted = false;
    private Vector3 m_startPosition;
    private Vector3 m_endPosition;

    public Transform m_startModel;
    public Transform m_endModel;
    public Transform m_segmentModel;

    private List<Transform> m_segments;

    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = new Vector3(-1, -1, -1);
        m_endPosition = new Vector3(-1, -1, -1);

        m_segments = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //stretch pathway from startpos to mousepos
        if(m_isStarted)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 mousePos = new Vector3(0, 0, 0);

            if (Physics.Raycast(ray, out hit))
            {
                mousePos = hit.point;
                Debug.DrawLine(m_startPosition, mousePos, Color.red);
            }
        }
    }

    public override Transform GetPreview()
    {
        if (m_isStarted)
            return Instantiate(m_endModel);
        else
            return Instantiate(m_startModel);
    }

    public override void Place(Vector3 position)
    {
        if(!m_isStarted)
        {
            IsPlacing(true);
            Debug.Log("Starting pathway placement");
            m_startPosition = position;
            m_isStarted = true;
            Transform start = Instantiate(m_startModel);
            start.transform.position = position;
            start.transform.parent = this.transform;

        }
        else
        {
            Debug.Log("Ending pathway placement");
            m_endPosition = position;
            m_isStarted = false;
            Transform end = Instantiate(m_endModel);
            end.transform.position = position;
            end.transform.parent = this.transform;
            //Fill in the middle
            FillInPath();
            IsPlacing(false);
        }
    }

    private void FillInPath()
    {
        Vector3 pathVec = -(m_startPosition - m_endPosition);
        Vector3 normPathVec = pathVec.normalized;

        //The segment size
        float increment = m_segmentModel.transform.localScale.x;
        float pathSoFar = 1f;

        while(pathSoFar < pathVec.magnitude)
        {
            Vector3 segLoc = m_startPosition + (normPathVec * pathSoFar);
            pathSoFar += increment;
            Transform seg = Instantiate(m_segmentModel);
            seg.transform.position = segLoc;
            seg.transform.parent = this.transform;
            m_segments.Add(seg);
        }
    }

    public override void DestroyThis()
    {
        Destroy(m_startModel);
        Destroy(m_endModel);

        foreach(Transform obj in m_segments)
        {
            Destroy(obj);
        }
    }

    public override void CancelPlacement()
    {
        if(m_isStarted)
        {
            Destroy(m_startModel);
            m_isStarted = false;
        }
    }
}
