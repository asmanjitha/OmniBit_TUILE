using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicatorARF : MonoBehaviour
{
    public ARSessionOrigin _arSessionOrigin;
    // public GameObject _placementIndicator;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARRaycastManager _arRaycastManager;
    private GameObject spawnedObject;
    private static Vector2 rayOrigin;
    public static bool isActive = false;
    public bool returnTargetingobject = false;

    private Material mat;
    Color showColor;
    Color hideColor;

    public static PlacementIndicatorARF instance = null;
    public static GameObject _targetedGameObject = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            PlaceIndicator();
            if (returnTargetingobject)
            {
                ReturnTargetedGameObject();
            }

        }
    }

    void LateUpdate()
    {

    }

    public static void Reset()
    {
        isActive = false;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (_arSessionOrigin.GetComponent<ARRaycastManager>() != null)
        {
            _arRaycastManager = _arSessionOrigin.GetComponent<ARRaycastManager>();
        }
        else
        {
            _arSessionOrigin.gameObject.AddComponent<ARRaycastManager>();
            _arRaycastManager = _arSessionOrigin.GetComponent<ARRaycastManager>();
        }
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        rayOrigin = screenCenter;

        mat = gameObject.GetComponentInChildren<MeshRenderer>().material;
        Color matColor = mat.GetColor("_Color");
        showColor = new Color(matColor.r, matColor.g, matColor.b, 1.0f);
        hideColor = new Color(matColor.r, matColor.g, matColor.b, 0.0f);


    }

    public void PlaceIndicator()
    {

        if (_arRaycastManager.Raycast(rayOrigin, hits, TrackableType.PlaneWithinPolygon))
        {
            ShowMarker();
            var hitPose = hits[0].pose;
            gameObject.transform.position = hitPose.position;
            // ARPlane plane = ScanARSceneManager._instance.planeManager.GetPlane(hits[0].trackableId);
            // if (plane.alignment == PlaneAlignment.Vertical)
            // {
            //     gameObject.transform.eulerAngles = new Vector3(plane.normal.x, 1, plane.normal.z) * 90.0f;
            // }
            // else
            // {
            //     gameObject.transform.eulerAngles = Vector3.zero;
            // }

        }
        else
        {
            HideMarker();
        }

    }

    public static bool IsTargetingGameObjectWithinRange(GameObject target, float range)
    {
        float dist = Vector3.Distance(instance.gameObject.transform.position, target.transform.position);
        if (dist < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static void ReturnTargetedGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(rayOrigin);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            _targetedGameObject = hit.transform.gameObject;
        }
        else
            _targetedGameObject = null;


    }

    private void HideMarker()
    {
        mat.SetColor("_Color", hideColor);
    }

    private void ShowMarker()
    {

        mat.SetColor("_Color", showColor);
    }

    public static void Activate()
    {
        instance.ShowMarker();
        isActive = true;
    }
    public static void Deactivate()
    {
        _targetedGameObject = null;
        instance.HideMarker();
        isActive = false;
    }


}
