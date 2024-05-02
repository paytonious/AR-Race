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

        _car.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        rigidBody = _car.GetComponent<Rigidbody>();
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
        ScreenLog.Log("Placing car at " + touchPosition);

        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            _car.transform.position = hitPose.position;
            _car.transform.rotation = hitPose.rotation;
            // placedPrefab = Instantiate(_car, hitPose.position, hitPose.rotation);
        }

        isPlaced = true;
    }

    void Update() {
        movement = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
    }

    void fixedUpdate() {
        rigidBody.transform.Translate(movement * _moveSpeed * Time.fixedDeltaTime, Space.World);

        if (_joystick.Horizontal != 0 || -_joystick.Vertical != 0) {
             _car.transform.rotation = Quaternion.Slerp(
                _car.transform.rotation,
                Quaternion.LookRotation(movement),
                Time.fixedDeltaTime * 5.0f);
        }
    }
}