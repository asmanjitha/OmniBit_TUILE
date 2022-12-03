using UnityEngine;
using System.Collections;


public class PlaneFromPoly : MonoBehaviour
{

    public Material mat;
    public Vector3[] poly;  // Initialized in the inspector

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // GeneratePlane(0);
        // poly = DetermineDirectionOfPolygon(poly);
        // GeneratePlane(0);
        // DetermineMaximumLengths(poly);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

    }
    public void SetMaterial(Material material)
    {
        mat = material;
    }

    // public void SetPoly(Vector3[] polygons){
    //     poly = polygons;
    //     string msg =  poly.ToString();
    //     ToastMessage.ShowToastMessage("Poly set:" + msg);
    // }
    public void GeneratePlane(int alignment, Vector3[] poly, Material mat)
    {
        if (poly == null || poly.Length < 3)
        {
            ToastMessage.ShowToastMessage("Define 2D polygon in 'poly' in the the Inspector");
            return;
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

        plane.transform.position = center;
        plane.transform.SetParent(GameObject.Find("Generated Planes").transform);

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();



    }

    Vector3 FindCenter(Vector3[] poly)
    {
        Vector3 center = Vector3.zero;
        foreach (Vector3 v3 in poly)
        {
            center += v3;
        }
        return center / poly.Length;
    }

    Vector2[] BuildUVsVerticle(Vector3[] vertices)
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

    Vector2[] BuildUVsHorizontal(Vector3[] vertices)
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

    // private Vector3[] DetermineDirectionOfPolygon(Vector3[] poly){
    //     if(poly.Length < 3){
    //         return null;
    //     }else{
    //         float sum = 0;
    //         Vector3[] reverse = new Vector3[poly.Length];
    //         for(int i=0; i<poly.Length-1;i++){
    //             float val = (poly[i+1].x - poly[i].x) * (poly[i+1].z + poly[i].z);
    //             sum += val;
    //             reverse[reverse.Length-i-1] = poly[i];
    //         }
    //         float valLast = (poly[0].x - poly[poly.Length-1].x) * (poly[0].z + poly[poly.Length-1].z);
    //         reverse[0] = poly[poly.Length-1];

    //         if(sum > 0){
    //             return poly;
    //         }else{
    //             return reverse;
    //         }
    //     }
    // }
    // public void DetermineMaximumLengths(Vector3[] poly){
    //     Debug.Log("Running");
    //     float maxX = 1;
    //     float maxZ = 1;

    //     for (int i = 0; i < poly.Length-1; i++){
    //         var tempX = Mathf.Abs(poly[i].x - poly[i+1].x);
    //         var tempZ = Mathf.Abs(poly[i].z - poly[i+1].z);

    //         if(tempX > maxX){
    //             maxX = tempX;
    //         }
    //         if(tempZ > maxZ){
    //             maxZ = tempZ;
    //         }
    //     }

    //     maxX = Mathf.RoundToInt(maxX);
    //     maxZ = Mathf.RoundToInt(maxZ);
    //     Debug.Log(maxX + "," +  maxZ);

    //     mat.mainTextureScale = new Vector2(maxX,maxZ);
    // }
}