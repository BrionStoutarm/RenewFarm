using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject groundSeg;
    public int m_width, m_height, m_depth;

    private PlacementGrid m_grid;

    private void Awake()
    {
        m_grid = FindObjectOfType<PlacementGrid>();
        if (m_grid)
            Debug.Log("Got Grid");
    }

    void Start()
    {
        GenerateMap(m_width, m_height, m_depth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateMap(int width = 0, int height = 0, int depth = 0)
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                for(int k = 0; k < depth; k++)
                {
                    Vector3 vec = new Vector3(i + 1, j + 1, k + 1);
                    Instantiate(groundSeg, m_grid.GetNearestPointOnGrid(vec), Quaternion.identity);
                }
            }
        }
    }
}
