using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;


[RequireComponent(typeof(ARRaycastManager))]
public class PlaceARAnchor : MonoBehaviour
{
    public GameObject anchorPrefab;
    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 rayOrigin;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public static List<GameObject> anchors = new List<GameObject>();

    [SerializeField]
    private LineRenderer lineRenderer;

    public LineRenderer tempLineRenderer;
    public static PlaceARAnchor instance = null;
    private static bool isScanning = true;

    // [SerializeField]
    // private TMP_Text tempAnchorDistanceText;

    // public GameObject distanceLabel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _arRaycastManager = GetComponent<ARRaycastManager>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        UpdateTempLineRenderer(PlacementIndicatorARF.instance.gameObject.transform.position);
    }

    public static void Reset()
    {
        anchors = new List<GameObject>();
        isScanning = true;
        instance = null;
    }

    public void PlaceAnchor()
    {
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        rayOrigin = screenCenter;
        if (_arRaycastManager.Raycast(rayOrigin, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (anchors.Count > 2 && PlacementIndicatorARF.IsTargetingGameObjectWithinRange(anchors[0], 0.05f))
            {
                EndAnchoring();
            }
            else
            {
                GameObject anchor = Instantiate(anchorPrefab, hitPose.position, hitPose.rotation);
                anchors.Add(anchor);
                UpdateLineRenderer(hitPose);
                if (anchors.Count > 1)
                {
                    int count = anchors.Count;
                    if (count > 2)
                    {
                        ARSceneManager.ActivateCompleteAnchoringButton();
                    }
                    // string dist = GetDistanceWithUnit(anchors[count - 2].transform.position, anchors[count - 1].transform.position);
                    // MakeDistancelabel(anchors[count - 2].transform.position, anchors[count - 1].transform.position, dist);
                }
            }

        }
        else
        {
            ToastMessage.ShowToastMessage("No Plane detected");
        }
    }

    public void EndAnchoring()
    {
        isScanning = false;
        Pose pose = new Pose(anchors[0].transform.position, anchors[0].transform.rotation);
        UpdateLineRenderer(pose);
        tempLineRenderer.SetPosition(0, Vector3.zero);
        tempLineRenderer.SetPosition(1, Vector3.zero);
        PlacementIndicatorARF.Deactivate();
        // string dist = GetDistanceWithUnit(anchors[0].transform.position, anchors[anchors.Count - 1].transform.position);
        // MakeDistancelabel(anchors[0].transform.position, anchors[anchors.Count - 1].transform.position, dist);
        ARSceneManager.CloseAnchorPlacingMode();
        // tempAnchorDistanceText.text = "";
        Invoke("ResetAnchorSizes", 0.5f);
    }

    private void ResetAnchorSizes()
    {
        foreach (GameObject anchor in anchors)
        {
            anchor.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }

    public void UpdateLineRenderer(Pose position)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position.position);
        tempLineRenderer.SetPosition(0, position.position);
    }

    private void MakeDistancelabel(Vector3 a, Vector3 b, string distance)
    {
        // GameObject temp = Instantiate(distanceLabel);
        // temp.GetComponent<DistanceLabelBehaviour>().PopulateDistanceLabel(a, b, distance);
    }

    public void UpdateTempLineRenderer(Vector3 position)
    {
        if (isScanning && anchors.Count > 0)
        {
            tempLineRenderer.SetPosition(1, position);
            // tempAnchorDistanceText.text = GetDistanceWithUnit(tempLineRenderer.GetPosition(0), tempLineRenderer.GetPosition(1));

            if (anchors.Count > 2 && PlacementIndicatorARF.IsTargetingGameObjectWithinRange(anchors[0], 0.05f))
            {
                anchors[0].transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            }
            else
            {
                anchors[0].transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            }
        }



    }

    private float GetDistance(Vector3 p1, Vector3 p2)
    {
        return (float)Vector3.Distance(p1, p2);
    }

    // private string GetDistanceWithUnit(Vector3 a, Vector3 b)
    // {
    //     float d = GetDistance(a, b);
    //     if (ScanARSceneManager.measuringUnit == Names.CENTIMETER)
    //     {
    //         return (d * 100).ToString() + Names.CENTIMETER;
    //     }
    //     else if (ScanARSceneManager.measuringUnit == Names.METER)
    //     {
    //         return d.ToString() + Names.METER;
    //     }
    //     else if (ScanARSceneManager.measuringUnit == Names.INCH)
    //     {
    //         return (d * 39.3701f).ToString() + Names.INCH;
    //     }
    //     else
    //     {
    //         return 0.ToString();
    //     }
    // }
}
