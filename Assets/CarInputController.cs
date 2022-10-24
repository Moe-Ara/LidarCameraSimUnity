using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for controlling the car with the WASD or Gamepad Controls.
/// </summary>
public class CarInputController : MonoBehaviour
{
    private Vector2 _moveDirection;

    public void Start()
    {_moveDirection=Vector2.zero;
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        
        // transform.Translate(new Vector3(_moveDirection.x, 0, _moveDirection.y) * 15f * Time.deltaTime);
        MoveCar(_moveDirection.x, _moveDirection.y);
        Debug.Log(_moveDirection.x + " " + _moveDirection.y );
    }

    private void MoveCar(float x, float y)
    {
        var speed = 15f;
        rearLeftCollider.motorTorque = speed * 200.0f ;
        rearRightCollider.motorTorque = speed * 200.0f ;
        frontLeftCollider.steerAngle = y * 45.0f;
        frontRightCollider.steerAngle = y * 45.0f;
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
        // MoveCar(_moveDirection.x, _moveDirection.y);
        // // Get Steering
        // var steering = Input.GetAxis("Horizontal");
        // var gas = Input.GetAxis("Vertical");
        // // Get Gas
        //
        // // Apply forces in NM 
        // rearLeftCollider.motorTorque = gas * 200.0f;
        // rearRightCollider.motorTorque = gas * 200.0f;
        //
        // // Steering
        // frontLeftCollider.steerAngle = steering * 45.0f;
        // frontRightCollider.steerAngle = steering * 45.0f;
        //
        // // Update Transforms
        // //frontLeftTransform.rotation = frontLeftCollider.transform.rotation;
        // //frontRightTransform.rotation = frontRightCollider.transform.rotation;
        // //rearLeftTransform.rotation = rearLeftCollider.transform.rotation;
        // //rearRightTransform.rotation = rearRightTransform.transform.rotation;
    }


}