using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class CarController : MonoBehaviour
{
    public Rigidbody _rigidBody;
    public FixedJoystick _joystick;
    public float _moveSpeed;
    // Start is called before the first frame update
    void fixedUpdate() {
        _rigidBody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidBody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || -_joystick.Vertical != 0) {
            transform.rotation = Quaternion.LookRotation(_rigidBody.velocity);
        }
    }
}