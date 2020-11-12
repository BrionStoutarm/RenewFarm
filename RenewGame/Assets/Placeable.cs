using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;
public class Placeable : MonoBehaviour
{
    public Color startColor;
    public Color highlightColor;

    private bool m_isPlacing;
    // Start is called before the first frame update
    void Start()
    {
        m_isPlacing = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public virtual void Place(Vector3 position)
    {
        Instantiate(this.gameObject).transform.position = position;
        IsPlacing(false);
    }

    public virtual GameObject GetPreview()
    {
        GameObject obj = Instantiate(this.gameObject);
        return obj;
    }


    public void IsPlacing(bool val)
    {
        m_isPlacing = val;
    }

    public bool IsPlacing()
    {
        return m_isPlacing;
    }    
}
