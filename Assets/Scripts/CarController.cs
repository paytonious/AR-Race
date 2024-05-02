using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class CarController : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycaster;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject _car;
    public FixedJoystick _joystick;
    public float _moveSpeed;


    private GameObject placedPrefab;
    private Rigidbody rigidBody;
    private bool isPlaced;
    private Vector3 movement;
    
    public void Start() {
        UIController.ShowUI("ControlCar");
        ScreenLog.Log("Started Car Controller");
        isPlaced = false;
    }

    public void OnPlaceObject(InputValue value)
    {
        if(!isPlaced) {
            ScreenLog.Log("placing car");
            Vector2 touchPosition = value.Get<Vector2>();
            PlaceObject(touchPosition);
        }
    }

    void PlaceObject(Vector2 touchPosition)
    {
        ScreenLog.Log("Placing Object at " + touchPosition);

        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            placedPrefab = Instantiate(_car, hitPose.position, hitPose.rotation);
            rigidBody = placedPrefab.GetComponent<Rigidbody>();
            placedPrefab.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }

        isPlaced = true;
    }

    void Update() {
        movement = new Vector3(_joystick.Horizontal, rigidBody.velocity.y, _joystick.Vertical).normalized;
    }

    void fixedUpdate() {
        rigidBody.velocity = movement * _moveSpeed * Time.fixedDeltaTime;

        if (_joystick.Horizontal != 0 || -_joystick.Vertical != 0) {
            transform.rotation = Quaternion.LookRotation(rigidBody.velocity);
        }
    }
}