// using System.Collections.Generic;
// using UnityEngine;
// using GoogleARCore;

// public class SurfaceSelector : MonoBehaviour
// {
//     [SerializeField] private PlacementIndicatorBehaviour placementIndicator;
//     [SerializeField] private GameObject horizontalMarker;
//     [SerializeField] private GameObject verticalMarker;
//     public static List<GameObject> verticlePoints;
//     public List<GameObject> horizontalPoints;
//     private PlaneFromPoly planeFromPoly;
//     private Vector3[] poly;


//     private Vector2 materialData = new Vector2(1, 1);

//     [SerializeField] private Material material1;
//     // [SerializeField] private Material material2;
//     // Start is called before the first frame update
//     void Start()
//     {
//         planeFromPoly = gameObject.GetComponent<PlaneFromPoly>();
//     }


//     void Update()
//     {

//     }

//     public void PlaceMarkerOnSerface()
//     {
//         TrackableHit hit = placementIndicator.GetHitDetails();
//         if (hit.Trackable is DetectedPlane)
//         {
//             DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
//             if (detectedPlane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing)
//             {
//                 GameObject point = placementIndicator.PlaceObjectAtPlacementPoseWithReturn(horizontalMarker, hit);
//                 horizontalPoints.Add(point);
//                 VisualizeLine(point, horizontalPoints);
//             }
//         }
//     }

//     public void HideMarkerPoints()
//     {
//         foreach (GameObject point in horizontalPoints)
//         {
//             point.SetActive(false);
//         }
//     }



//     public void VisualizeMarkeredPlane()
//     {
//         List<GameObject> points = horizontalPoints;
//         Vector3[] poly = new Vector3[points.Count];
//         for (int i = 0; i < points.Count; i++)
//         {
//             poly[i] = points[i].transform.position;
//         }
//         poly = DetermineDirectionOfPolygon(poly);
//         DetermineMaximumLenghts(poly);
//         planeFromPoly.GeneratePlane(0, poly, material1);
//         HideMarkerPoints();
//     }

//     private void VisualizeLine(GameObject point, List<GameObject> points)
//     {
//         if (horizontalPoints.Count >= 2)
//         {
//             point.GetComponent<LineRenderer>().positionCount = 2;
//             point.GetComponent<LineRenderer>().SetPosition(0, point.transform.position);
//             point.GetComponent<LineRenderer>().SetPosition(1, horizontalPoints[horizontalPoints.Count - 2].transform.position);
//         }
//         if (points.Count >= 3)
//         {
//             GameObject firstPoint = points[0];
//             firstPoint.GetComponent<LineRenderer>().positionCount = 2;
//             firstPoint.GetComponent<LineRenderer>().SetPosition(0, firstPoint.transform.position);
//             firstPoint.GetComponent<LineRenderer>().SetPosition(1, points[points.Count - 1].transform.position);
//         }
//     }

//     private Vector3[] DetermineDirectionOfPolygon(Vector3[] poly)
//     {
//         if (poly.Length < 3)
//         {
//             return null;
//         }
//         else
//         {
//             float sum = 0;
//             Vector3[] reverse = new Vector3[poly.Length];
//             for (int i = 0; i < poly.Length - 1; i++)
//             {
//                 float val = (poly[i + 1].x - poly[i].x) * (poly[i + 1].z + poly[i].z);
//                 sum += val;
//                 reverse[reverse.Length - i - 1] = poly[i];
//             }
//             float valLast = (poly[0].x - poly[poly.Length - 1].x) * (poly[0].z + poly[poly.Length - 1].z);
//             reverse[0] = poly[poly.Length - 1];

//             if (sum > 0)
//             {
//                 return poly;
//             }
//             else
//             {
//                 return reverse;
//             }
//         }
//     }

//     public void changeMaterial(Material mat)
//     {
//         mat.mainTextureScale = materialData;
//         GameObject.Find("Generated Planes").transform.Find("Generated Plane").GetComponent<MeshRenderer>().material = mat;
//     }

//     public void DetermineMaximumLenghts(Vector3[] poly)
//     {
//         float maxX = 1;
//         float maxZ = 1;

//         for (int i = 0; i < poly.Length - 1; i++)
//         {
//             var tempX = Mathf.Abs(poly[i].x - poly[i + 1].x);
//             var tempZ = Mathf.Abs(poly[i].z - poly[i + 1].z);

//             if (tempX > maxX)
//             {
//                 maxX = tempX;
//             }
//             if (tempZ > maxZ)
//             {
//                 maxZ = tempZ;
//             }
//         }

//         maxX = Mathf.RoundToInt(maxX);
//         maxZ = Mathf.RoundToInt(maxZ);

//         materialData = new Vector2(maxX, maxZ);
//         material1.mainTextureScale = materialData;
//     }

// }
