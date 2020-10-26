using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : MonoBehaviour
{
    public Color startColor;
    //public Color startSideColor;
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

    private void OnMouseDown()
    {
        if(TerrainManipToggle.isTerrainManip())
            Destroy(this.gameObject);
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

        GetComponent<Renderer>().material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;

        Debug.Log("Mouse Exit");
    }
}
