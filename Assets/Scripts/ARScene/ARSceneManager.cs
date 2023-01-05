using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

public class ARSceneManager : MonoBehaviour
{
    public static ARSceneManager _instance;
    [SerializeField] private GameObject[] UIs;
    [SerializeField] private GameObject trackingUI;
    [SerializeField] private GameObject afterPlaneGenerateUI;
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private ARSession session;
    [SerializeField] private GameObject AnimaationBGPanel;
    [SerializeField] private VideoPlayer animationPlayer;
    private static GameObject generatePlane;
    public GameObject GeneratePlaneButton;
    public GameObject AddAnchorButton;
    public GameObject CompleteAnchorPlacementButton;
    public GameObject UndoPlacedAnchorButton;

    public Material defaultPlaneMat;
    private static bool _tracking = false;

    // Start is called before the first frame update
    void Start()
    {
        // Disable screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        foreach (GameObject ui in UIs)
        {
            ui.SetActive(false);
        }
        trackingUI.SetActive(true);
        planeManager.planesChanged += HideHandAnimation;
    }

    void OnDisable()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        ShowHandAnimation();
    }

    public void close()
    {
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_ANDROID)
            Application.Quit();
#elif (UNITYIOS)
            Application.Quit();
#endif
    }

    public ARPlaneManager planeManager
    {
        get { return _planeManager; }
        set { }
    }

    public static bool tracking
    {
        get { return _tracking; }
        set { _tracking = value; }
    }

    public void ReloadARScene()
    {
        session.Reset();
        PlaceARAnchor.anchors = new List<GameObject>();
    }

    public void HideHandAnimation(ARPlanesChangedEventArgs args)
    {
        if (args.added.Count > 0)
        {
            animationPlayer.Stop();
            AnimaationBGPanel.SetActive(false);
            tracking = true;
        }
    }

    public void ShowHandAnimation()
    {
        AnimaationBGPanel.GetComponentInChildren<RawImage>().color = new Color32(255, 255, 255, 255);
    }

    public void LoadMaterialButtons()
    {
        Debug.Log("Loading Buttons");
        foreach (string material in Data.getMaterials())
        {
            GameObject button = Resources.Load<GameObject>("MaterialButton/MaterialButton");
            GameObject newButton = Instantiate(button);
            MaterialButtonBehaviour buttonBehaviour = newButton.GetComponent<MaterialButtonBehaviour>();
            buttonBehaviour.materialName = material;
            buttonBehaviour.materialTex = Resources.Load<Texture>("Tile Materials/" + material + "/" + material);
            buttonBehaviour.material = Resources.Load<Material>("Tile Materials/" + material + "/" + material + "_MAT");
            buttonBehaviour.InitiateButton();
        }
    }

    public void LoadGallery()
    {
        SceneManager.LoadSceneAsync("Gallery");
    }

    public static void CloseAnchorPlacingMode()
    {
        tracking = false;
        _instance.CompleteAnchorPlacementButton.SetActive(false);
        _instance.UndoPlacedAnchorButton.SetActive(false);
        _instance.GeneratePlaneButton.SetActive(true);
        _instance.AddAnchorButton.SetActive(false);

    }

    public void AccpetMarkedAnchorsAndGeneratePlane()
    {
        StartCoroutine("GeneratePlane");
        PlacementIndicatorARF.isActive = false;
        trackingUI.SetActive(false);
        afterPlaneGenerateUI.SetActive(true);
        LoadMaterialButtons();
    }

    IEnumerator GeneratePlane()
    {
        List<GameObject> anchors = PlaceARAnchor.anchors;
        List<Vector3> polyList = new List<Vector3>();
        foreach (var anchor in anchors)
        {
            polyList.Add(anchor.transform.position);
        }
        Vector3[] poly = polyList.ToArray();

        Vector3 center = PlaneFromPoly.FindCenter(poly);

        GameObject plane = PlaneFromPoly.GeneratePlane(0, poly, defaultPlaneMat);
        plane.transform.position = center;
        plane.name = "GeneratedPlane";

        yield return plane;
    }

    public void CompletePolygon()
    {
        PlaceARAnchor.instance.EndAnchoring();
    }

    public void UndoPlacedAnchor()
    {
        PlaceARAnchor.instance.UndoPlacedAnchor();
    }
}
