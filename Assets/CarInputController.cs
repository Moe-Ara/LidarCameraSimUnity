using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for controlling the car with the WASD or Gamepad Controls.
/// </summary>
public class CarInputController : MonoBehaviour
{
    private Vector2 _moveDirection;

    public void Start()
    {
        _moveDirection = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>() * new Vector2(1, 10);
    }

    /// <summary>
    /// The colliders of the front wheels (for steering)
    /// </summary>
    public WheelCollider frontLeftCollider, frontRightCollider;

    public Transform frontLeftTransform, frontRightTransform;


    /// <summary>
    /// The colliders of the rear wheels (for thrust)
    /// </summary>
    public WheelCollider rearLeftCollider, rearRightCollider;

    public Transform rearLeftTransform, rearRightTransform;

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Move using Wheel Collider
        rearLeftCollider.motorTorque = 10.0f * _moveDirection.y;
        rearRightCollider.motorTorque = 10.0f * _moveDirection.y;
        frontLeftCollider.steerAngle = 45.0f * _moveDirection.x;
        frontRightCollider.steerAngle = 45.0f * _moveDirection.x;
    }

    public void ResetCar()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}