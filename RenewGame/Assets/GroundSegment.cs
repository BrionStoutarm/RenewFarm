using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : Placeable
{
    public Color startColor;
    //public Color startSideColor;
    public Color highlightColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if(TerrainManipToggle.isDestroy())
            Destroy(this.gameObject);
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            //Debug.Log("Missed");
            return;
        }

        GetComponent<Renderer>().material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;

        //Debug.Log("Mouse Exit");
    }
}
