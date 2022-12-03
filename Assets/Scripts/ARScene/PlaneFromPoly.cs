using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFromPoly
{

    // public Material mat;
    // public Vector3[] poly;  // Initialized in the inspector


    // public void SetMaterial(Material material)
    // {
    //     mat = material;
    // }

    public static GameObject GeneratePlane(int alignment, Vector3[] poly, Material mat)
    {
        if (poly == null || poly.Length < 3)
        {
            ToastMessage.ShowToastMessage("Define 2D polygon");
            return null;
        }

        GameObject plane = new GameObject("Generated Plane");

        MeshFilter mf = plane.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        Renderer rend = plane.AddComponent<MeshRenderer>();
        rend.material = mat;

        Vector3 center = FindCenter(poly);


        Vector3[] vertices = new Vector3[poly.Length + 1];
        vertices[0] = Vector3.zero;

        for (int i = 0; i < poly.Length; i++)
        {
            vertices[i + 1] = poly[i] - center;
        }

        mesh.vertices = vertices;

        int[] triangles = new int[poly.Length * 3];

        for (int i = 0; i < poly.Length - 1; i++)
        {
            triangles[i * 3] = i + 2;
            triangles[i * 3 + 1] = 0;
            triangles[i * 3 + 2] = i + 1;
        }

        triangles[(poly.Length - 1) * 3] = 1;
        triangles[(poly.Length - 1) * 3 + 1] = 0;
        triangles[(poly.Length - 1) * 3 + 2] = poly.Length;

        mesh.triangles = triangles;
        if (alignment == 0)
        {
            mesh.uv = BuildUVsHorizontal(vertices);
        }
        else
        {
            mesh.uv = BuildUVsVerticle(vertices);
        }


        // plane.transform.SetParent(GameObject.Find("3DObject").transform);
        plane.transform.localPosition = center;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();


        mesh.RecalculateTangents();
        return plane;
    }

    public static Vector3 FindCenter(Vector3[] poly)
    {
        Vector3 center = Vector3.zero;
        foreach (Vector3 v3 in poly)
        {
            center += v3;
        }
        return center / poly.Length;
    }

    static Vector2[] BuildUVsVerticle(Vector3[] vertices)
    {

        float xMin = Mathf.Infinity;
        float yMin = Mathf.Infinity;
        float xMax = -Mathf.Infinity;
        float yMax = -Mathf.Infinity;

        foreach (Vector3 v3 in vertices)
        {
            if (v3.x < xMin)
                xMin = v3.x;
            if (v3.y < yMin)
                yMin = v3.y;
            if (v3.x > xMax)
                xMax = v3.x;
            if (v3.y > yMax)
                yMax = v3.y;
        }

        float xRange = xMax - xMin;
        float yRange = yMax - yMin;

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i].x = (vertices[i].x - xMin) / xRange;
            uvs[i].y = (vertices[i].y - yMin) / yRange;

        }
        return uvs;
    }

    static Vector2[] BuildUVsHorizontal(Vector3[] vertices)
    {

        float xMin = Mathf.Infinity;
        float yMin = Mathf.Infinity;
        float xMax = -Mathf.Infinity;
        float yMax = -Mathf.Infinity;

        foreach (Vector3 v3 in vertices)
        {
            if (v3.x < xMin)
                xMin = v3.x;
            if (v3.z < yMin)
                yMin = v3.z;
            if (v3.x > xMax)
                xMax = v3.x;
            if (v3.z > yMax)
                yMax = v3.z;
        }

        float xRange = xMax - xMin;
        float yRange = yMax - yMin;

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i].x = (vertices[i].x - xMin) / xRange;
            uvs[i].y = (vertices[i].z - yMin) / yRange;

        }
        return uvs;
    }

}
