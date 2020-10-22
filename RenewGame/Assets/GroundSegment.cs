using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : MonoBehaviour
{
    public Color startColor;
    public Color highlightColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;
    }
}
