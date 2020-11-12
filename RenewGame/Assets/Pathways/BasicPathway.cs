using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPathway : Placeable
{
    private bool m_isStarted = false;
    private Vector3 m_startPosition;
    private Vector3 m_endPosition;

    private GameObject m_startBlock;
    private GameObject m_endBlock;
    private GameObject m_segmentBlock;

    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = new Vector3(-1, -1, -1);
        m_endPosition = new Vector3(-1, -1, -1);

        m_startBlock = GameObject.Find("StartPathBlock");
        m_endBlock = GameObject.Find("EndPathBlock");
        m_segmentBlock = GameObject.Find("SegmentPathBlock");
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

    public override GameObject GetPreview()
    {
        if (m_isStarted)
            return Instantiate(m_endBlock);
        else
            return Instantiate(m_startBlock);
    }

    public override void Place(Vector3 position)
    {
        if(!m_isStarted)
        {
            Debug.Log("Starting pathway placement");
            m_startPosition = position;
            IsPlacing(true);
            m_isStarted = true;
            Instantiate(m_startBlock).transform.position = position;
        }
        else
        {
            Debug.Log("Ending pathway placement");
            m_endPosition = position;
            IsPlacing(false);
            m_isStarted = false;
            Instantiate(m_endBlock).transform.position = position;
        }
    }
}
