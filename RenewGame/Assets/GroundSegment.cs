using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : Placeable
{

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
        if(ObjectPlaceTools.isDestroy())
            Destroy(this.gameObject);
    }

}
