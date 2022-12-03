// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DoneSelectionButtonBehaviour : MonoBehaviour
// {
//     [SerializeField] private SurfaceSelector surfaceSelector;
//     [SerializeField] private PlacementIndicatorBehaviour placementIndicator;
//     [SerializeField] private GameObject afterUI;
//     [SerializeField] private GameObject trackingUI;


//     /// <summary>
//     /// Start is called on the frame when a script is enabled just before
//     /// any of the Update methods is called the first time.
//     /// </summary>
//     void Start()
//     {
//         placementIndicator = GameObject.Find("Placement Indicator").GetComponent<PlacementIndicatorBehaviour>();
//     }
//     public void VisualizeMarkeredPlane(){
//         StartCoroutine("PlaneVisualizeMethod");
//     }

//     IEnumerator PlaneVisualizeMethod(){
//         afterUI.SetActive(true);
//         // afterUI.SetActive(true);
//         trackingUI.SetActive(false);

//         placementIndicator.PauseTracking();
//         surfaceSelector.VisualizeMarkeredPlane();

//         GameObject.Find("Scene Manager").GetComponent<ARSceneManager>().LoadMaterialButtons();
//         yield return null;
//     }

// }
