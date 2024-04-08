using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectOnPlane : MonoBehaviour
{
  [SerializeField] GameObject placedPrefab;
  GameObject spawnedObject;
  ARRaycastManager raycaster;
  List<ARRaycastHit> hits = new List<ARRaycastHit>();

  void OnPlaceObject(InputValue value)
  {
    ScreenLog.Log("placing an object on plane");
    // get the screen touch position
    Vector2 touchPosition = value.Get<Vector2>();
    // raycast from the touch position into the 3D scene looking for a plane
    // if the raycast hits a plane, then
    if(raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
    {
      ScreenLog.Log("Raycast hit detected");
      // get the hit point (pose) on the plane
      Pose hitPose = hits[0].pose;
      // if this is the first time placing an object,
      if (spawnedObject == null)
      {
        ScreenLog.Log("Creating first prefab");
        // instantiate the prefab at the hit position and rotation
        spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
      }
      else
      {
        ScreenLog.Log("Moving prefab");
        // change the position of the previously placed object
        spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
      }
    }
  }
  // Start is called before the first frame update
  void Start()
  {
    raycaster = GetComponent<ARRaycastManager>();
    ScreenLog.Log("Created raycaster: " + raycaster);
    // spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
  }
}
