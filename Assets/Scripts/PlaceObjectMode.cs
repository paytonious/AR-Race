using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectMode : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycaster;
    [SerializeField] GameObject startStrip;
    [SerializeField] GameObject checkpointFlag;
    [SerializeField] RaceTrack raceTrack;
    private GameObject placedPrefab;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void OnEnable()
    {
        UIController.ShowUI("PlaceObject");
    }

    public void OnPlaceObject(InputValue value)
    {
        ScreenLog.Log("placing object");
        Vector2 touchPosition = value.Get<Vector2>();
        PlaceObject(touchPosition);
    }

    void PlaceObject(Vector2 touchPosition)
    {
        ScreenLog.Log("Placing Object at " + touchPosition);

        if(raceTrack.getNumCheckpoints() == 0) {
            placedPrefab = startStrip;
        }
        else {
            placedPrefab = checkpointFlag;
        }

        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            
            GameObject checkpoint = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            checkpoint.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            raceTrack.addCheckpoint(checkpoint);

            InteractionController.EnableMode("PlaceObject");
        }
    }
}
