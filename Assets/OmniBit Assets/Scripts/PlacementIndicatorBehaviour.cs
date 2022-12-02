using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class PlacementIndicatorBehaviour : MonoBehaviour
{
    private bool pauseTracking = false;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private GameObject horizontalPlacementIndicator;
    [SerializeField] private GameObject verticalPlacementIndicator;
    private static GameObject instantiatedIndicator;
    public static TrackableHit hit;
    private static float tracktime = 0;
    private static float nonTrackingTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
    }

    private void UpdatePlacementPose(){
        if (Session.Status != SessionStatus.Tracking || pauseTracking)
        {
            return;
        }else{
            TrackableHitFlags raycastFilter = TrackableHitFlags.Default;
            bool isAcceptableHit = false;

            Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            isAcceptableHit = Frame.Raycast(screenCenter.x, screenCenter.y, raycastFilter, out hit);
            
            if(isAcceptableHit){
                if(instantiatedIndicator != null && instantiatedIndicator.activeSelf == false){
                    instantiatedIndicator.SetActive(true);
                }
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(firstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    ToastMessage.ShowToastMessage("Hit at back of the current DetectedPlane");
                }

                if(hit.Trackable is DetectedPlane){
                    DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                    if (detectedPlane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing){

                        // Instantiate prefab at the hit pose.
                        if(instantiatedIndicator != null){
                            instantiatedIndicator.transform.position = hit.Pose.position;
                            instantiatedIndicator.transform.rotation = hit.Pose.rotation;
                        }else{
                            instantiatedIndicator = Instantiate(horizontalPlacementIndicator, hit.Pose.position, hit.Pose.rotation);
                        }
                        tracktime += Time.deltaTime;
                        nonTrackingTime = 0;
                    }else if (detectedPlane.PlaneType == DetectedPlaneType.Vertical){

                        // Instantiate prefab at the hit pose.
                        if(instantiatedIndicator != null){
                            instantiatedIndicator.transform.position = hit.Pose.position;
                            instantiatedIndicator.transform.rotation = hit.Pose.rotation;
                        }else{
                            instantiatedIndicator = Instantiate(verticalPlacementIndicator, hit.Pose.position, hit.Pose.rotation);
                        }
                        tracktime += Time.deltaTime;
                        nonTrackingTime = 0;
                    }
                }
            }else{
                if(instantiatedIndicator != null){
                    instantiatedIndicator.SetActive(false);
                    tracktime = 0;
                    nonTrackingTime += Time.deltaTime;
                }
            }
        }
        
    }

    public TrackableHit GetHitDetails(){
        return hit;
    }

    public float GetTrackingTime(){
        return tracktime;
    }

    public float GetNonTrackingTime(){
        return nonTrackingTime;
    }

    public bool IsTracking(){
        return !pauseTracking;
    }
    public void PauseTracking(){
        if(instantiatedIndicator != null){
            instantiatedIndicator.SetActive(false);
            pauseTracking = true;
        }
        
    }

    public void ActivateTracking(){
        if(instantiatedIndicator != null){
            instantiatedIndicator.SetActive(true);
            pauseTracking = false;
        }
        
    }

    public void PlaceObjectAtPlacementPose(GameObject prefab, TrackableHit hit){
        var gameObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

        gameObject.transform.parent = anchor.transform;
        if (hit.Trackable is InstantPlacementPoint)
        {
            gameObject.GetComponentInChildren<InstantPlacementEffect>()
                .InitializeWithTrackable(hit.Trackable);
        }
    }

    public GameObject PlaceObjectAtPlacementPoseWithReturn(GameObject prefab, TrackableHit hit){
        var gameObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

        gameObject.transform.parent = anchor.transform;
        if (hit.Trackable is InstantPlacementPoint)
        {
            gameObject.GetComponentInChildren<InstantPlacementEffect>()
                .InitializeWithTrackable(hit.Trackable);
        }
        return gameObject;
    }
}
