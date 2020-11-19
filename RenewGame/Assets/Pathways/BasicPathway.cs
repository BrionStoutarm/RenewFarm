using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;

public class BasicPathway : Placeable
{
    private bool m_isStarted = false;
    private Vector3 m_startPosition;
    private Vector3 m_endPosition;

    public Transform m_pathModel;
    private Transform m_currentPath;
    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = new Vector3(-1, -1, -1);
        m_endPosition = new Vector3(-1, -1, -1);
    }

    // Update is called once per frame
    void Update()
    {

        //stretch pathway from startpos to mousepos
        if(m_isStarted)
        {
            Vector3 mousePos = GetMousePoint();
            RotateTowardsMouse(mousePos);
            StretchToPoint(mousePos);
            Debug.DrawLine(m_startPosition, mousePos, Color.red);
        }
    }

    private Vector3 GetMousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 mousePos = new Vector3(0, 0, 0);

        if (Physics.Raycast(ray, out hit))
        {
            mousePos = hit.point;
        }

        return mousePos;
    }

    private void StretchToPoint(Vector3 mousePos)
    {
        Vector3 scale = new Vector3(1, 0.025f, 1);
        scale.z = Vector3.Distance(m_startPosition, mousePos);
        m_currentPath.localScale = scale;
    }

    private void RotateTowardsMouse(Vector3 mousePos)
    {
        m_currentPath.transform.forward = mousePos - m_startPosition;
    }

    public override Transform GetPreview()
    {
        return Instantiate(m_pathModel);
    }

    public override void Place(Vector3 position)
    {
        if(!m_isStarted)
        {
            Debug.Log("Starting pathway");
            m_currentPath = Instantiate(m_pathModel);
            m_currentPath.transform.position = position;
            m_startPosition = position;
            m_isStarted = true;
        }
        else
        {
            m_endPosition = position;
            Debug.Log("Ending pathway");
            m_currentPath = null;
            m_isStarted = false;
        }
    }

    private void FillInPath()
    {

    }

    public override void DestroyThis()
    {
        Destroy(this.gameObject);
        //Destroy(m_startModel);
        //Destroy(m_endModel);

        //foreach(Transform obj in m_segments)
        //{
        //    Destroy(obj);
        //}
    }

    public override void CancelPlacement()
    {
        if (m_isStarted)
        {
            Destroy(m_currentPath.gameObject);
            m_isStarted = false;
        }
    }
}
