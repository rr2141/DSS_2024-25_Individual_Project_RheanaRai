using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlaneDetectionWatcher : MonoBehaviour
{
    public GameObject playButtonCanvas; 
    private ARPlaneManager planeManager;
    private bool planeDetected = false;

    void Start()
    {
        planeManager = FindFirstObjectByType<ARPlaneManager>();

        if (playButtonCanvas != null)
            playButtonCanvas.SetActive(false); 
    }

    void Update()
    {
        if (!planeDetected && planeManager != null)
        {
            foreach (ARPlane plane in planeManager.trackables)
            {
                if (plane.trackingState == TrackingState.Tracking)
                {
                    planeDetected = true;
                    Debug.Log("Plane detected successfully");

                    if (playButtonCanvas != null)
                        playButtonCanvas.SetActive(true);
                    break;
                }
            }
        }
    }
}

