using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : MonoBehaviour
{
    public Color startTopColor;
    public Color startSideColor;
    public Color highlightColor;

    GameObject curHoveredSide;

    // Start is called before the first frame update
    void Start()
    {
        curHoveredSide = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Debug.Log("Missed");
            return;
        }

        GameObject side = hit.collider.gameObject;
        if(side)
        {
            side.GetComponent<Renderer>().material.color = highlightColor;
            curHoveredSide = side;
        }
    }

    private void OnMouseExit()
    {
        if(curHoveredSide)
        {
            if (curHoveredSide.name == "Top")
            {
                curHoveredSide.GetComponent<Renderer>().material.color = startTopColor;
            }
            else
                curHoveredSide.GetComponent<Renderer>().material.color = startSideColor;
        }
        curHoveredSide = null;
        Debug.Log("Mouse Exit");
    }
}
