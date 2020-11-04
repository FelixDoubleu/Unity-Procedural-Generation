using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshCreator1 : MonoBehaviour
{
    public int length = 0;
    public int width = 0;
    public float lengthOffset = 0;
    public float widthOffset = 0;
    public float Offset = 0;

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
                float y = Mathf.PerlinNoise(x * lengthOffset, z * widthOffset) * Offset;
                y += Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * 0.001f * y * y;
                y += Mathf.PerlinNoise(x * 0.1f, z * 0.1f) * 0.8f;
                y += Mathf.PerlinNoise(x * 0.5f, z * 0.5f) * 0.1f;
                y += y * y * 0.04f;
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
