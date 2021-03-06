﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public int length = 0;
    public int width = 0;
    public float maxHeight = 1;
    public float form = 1;

    private int lw = 0;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        CreateMesh();
        UpdateMesh();
    }

    void CreateMesh()
    {
        if(length < 1) { length = 2; }
        if(width < 1) { width = 2; }
        if (lw != (length - 1) * (width - 1) * 6)
        {
            Debug.Log((length - 1) * (width - 1) * 6);
        }
        lw = (length - 1) * (width - 1) * 6;
        vertices = new Vector3[length * width];
        triangles = new int[(length - 1) * (width - 1) * 6];
        
        int num_vert = 0;
        int num_tri = 0;
        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                if (x > 0 && z > 0)
                {
                    triangles[num_tri] = num_vert - width;
                    triangles[num_tri + 1] = num_vert - 1;
                    triangles[num_tri + 2] = num_vert - width - 1;
                    triangles[num_tri + 3] = num_vert;
                    triangles[num_tri + 4] = num_vert - 1;
                    triangles[num_tri + 5] = num_vert - width;
                    num_tri += 6;
                }
                float newx = x - length / 2;
                float newz = z - width / 2;
                float y = form * newx * newx + form * newz * newz;
                if (y > maxHeight)
                {
                    y = maxHeight;
                }
                vertices[num_vert] = new Vector3(x, y, z);
                num_vert++;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
