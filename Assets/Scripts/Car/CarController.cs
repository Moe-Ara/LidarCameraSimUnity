using System;
using Car.gss;
using Communication.Messages;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        private ControlResultMessage _control;
        private PIDController _pidController;
        private float _pidOutput;
        private float _currentSpeed;
        private GssController _gssController;

        /// <summary>
        /// The event that is triggered after a new car state was generated
        /// </summary>
        public Action<CarStateMessage> OnNewCarState;

        /// <summary>
        /// The actual front wheels
        /// </summary>
        public Transform frontLeftWheel, frontRightWheel;

        /// <summary>
        /// The colliders of the front wheels (for steering)
        /// </summary>
        public WheelCollider frontLeftCollider, frontRightCollider;

        /// <summary>
        /// The colliders of the rear wheels (for thrust)
        /// </summary>
        public WheelCollider rearLeftCollider, rearRightCollider;

        /// <summary>
        /// Important data for the car state
        /// </summary>
        private Vector3 _pos = Vector3.zero, _velocity = Vector3.zero, _rot = Vector3.zero;
        

        private void Start()
        {
            _currentSpeed = 0f;
            _pidOutput = 0f;
            _control = new ControlResultMessage();
            _pidController = gameObject.GetComponent<PIDController>();
            _gssController = gameObject.GetComponent<GssController>();
        }

        public GameManager manager;
        /// <summary>
        /// Update the car state
        /// </summary>
        private void FixedUpdate()
        {
            var trans = transform;

            // Calculate the velocity
            var lastPos = _pos;
            _pos = trans.position;
            _velocity = (_pos - lastPos) / Time.fixedDeltaTime;

            // _currentSpeed=_gssController.calculateSpeed();
            _currentSpeed=(float)Math.Round(_gssController.Speed, 3);
            // Calculate the angular velocity
            var lastRot = _rot;
            _rot = trans.rotation.eulerAngles;
            var angularVelocity = (_rot - lastRot) / Time.fixedDeltaTime;

            // Create the message
            var carState = new CarStateMessage
            {
                speed_actual = _velocity.magnitude,
                yaw_rate = -angularVelocity.y * Mathf.Deg2Rad
            };
            OnNewCarState?.Invoke(carState);
            _pidOutput=_pidController.calcPID(Time.fixedDeltaTime, _currentSpeed, _control.speed_target);
            Acceleration(_control);
        }

        /// <summary>
        /// Update the car
        /// </summary>
        /// <param name="control">The control result from the as</param>
        public void ApplyControlResult(ControlResultMessage control)
        {
            _control = control;
            //steering
            SetFrontWheelAngle(frontLeftCollider, frontLeftWheel, control.steering_angle_target);
            SetFrontWheelAngle(frontRightCollider, frontRightWheel, control.steering_angle_target);
            // TODO: Set max motor torque (motor_moment_target is a relative value)
            // rearLeftCollider.motorTorque = control.motor_moment_target * 50;
            // rearRightCollider.motorTorque = control.motor_moment_target * 50;
            // Accelerate; start driving
            // Steering();
            // Acceleration(control);
        }

        /// <summary>
        /// Set the rotation of the wheels including the colliders 
        /// </summary>
        /// <param name="wheelCollider">The wheel collider</param>
        /// <param name="wheelTransform">The visuals of the wheel</param>
        /// <param name="steeringAngle">The steering angle</param>
        private static void SetFrontWheelAngle(WheelCollider wheelCollider, Transform wheelTransform,
            float steeringAngle)
        {
            wheelCollider.steerAngle = steeringAngle;
            wheelTransform.rotation = Quaternion.Euler(0, steeringAngle, 90);
        }

        /// <summary>
        /// Start Acceleration; it is a helper method to be called each time we get data from autonomous system
        /// </summary>
        /// <param name="control">The control result from the as;i.e the values that we get from automation system</param>
        private void Acceleration(ControlResultMessage control)
        {
            if (control.speed_target != 0f)
            {
                rearLeftCollider.brakeTorque = 0;
                rearRightCollider.brakeTorque = 0;
                // rearLeftCollider.motorTorque = control.motor_moment_target * 50;
                rearLeftCollider.motorTorque =30;
                rearRightCollider.motorTorque =30;
            }
            else
            {
                Declaration();
            }
        }

        /// <summary>
        /// Start Declaration when the motor torque is 0
        /// </summary>
        /// <param name="control">The control result from the as;i.e the values that we get from automation system</param>
        private void Declaration()
        {
            var deceletaionForce =
                1f; // change this if you want the car to decelerate faster; the higher the force the faster the declaration
            rearLeftCollider.brakeTorque = deceletaionForce * 50;
            rearRightCollider.brakeTorque = deceletaionForce * 50;
        }
        /// <summary>
        /// Reset Car
        /// </summary>
        /// <param name="control">This method resets car to original position</param>
        public void ResetCar()
        {
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}