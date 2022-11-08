using System;
using Car.gss;
using Car.imu;
using Communication.Messages;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        #region Props

        public bool Automated
        {
            get => _automated;
            set => _automated = value;
        }

        #endregion

        private bool _braking;
        private Vector2 _moveDirection;
        private bool _automated = true;
        private ControlResultMessage _control;
        private PIDController _pidController;
        private float _pidOutput;
        private float _currentSpeed;
        public GssController _gssController;
        public Imu_Controller _ImuController;
        /// <summary>
        /// The event that is triggered after a new car state was generated
        /// </summary>
        public Action<CarStateMessage> OnNewCarState;

        /// <summary>
        /// The actual front wheels
        /// </summary>
        public Transform frontLeftWheel, frontRightWheel,rearRightWheel,rearLeftWheel;

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
            _moveDirection = Vector2.zero;
            _currentSpeed = 0f;
            _pidOutput = 0f;
            _control = new ControlResultMessage();
            _pidController = gameObject.GetComponent<PIDController>();
        }

        // public GameManager manager;
        /// <summary>
        /// Update the car state
        /// </summary>
        private void FixedUpdate()
        {
            var trans = transform;
            
            // Calculate the velocity
            _velocity = _gssController.Velocity;
            _currentSpeed = _gssController.Speed;
            // Calculate the angular velocity
            var lastRot = _rot;
            _rot = trans.rotation.eulerAngles;
            var angularVelocity = (_rot - lastRot) / Time.fixedDeltaTime;
            
            // Create the message
            var carState = new CarStateMessage
            {
                speed_actual = _currentSpeed,
                yaw_rate = -angularVelocity.y * Mathf.Deg2Rad
            };
            OnNewCarState?.Invoke(carState);
            _pidOutput=_pidController.calcPID(Time.fixedDeltaTime, _currentSpeed, _control.speed_target);
            // Debug.Log(_control.speed_target.ToString());
            if(_automated)
                Acceleration(_control);
            else
                MoveCarUsingInput();
        }

        /// <summary>
        /// Update the car
        /// </summary>
        /// <param name="control">The control result from the as</param>
        public void ApplyControlResult(ControlResultMessage control)
        {
            _control = control;
            if (!_automated) return;

        }

        /// <summary>
        /// Set the rotation of the wheels including the colliders 
        /// </summary>
        /// <param name="wheelCollider">The wheel collider</param>
        /// <param name="wheelTransform">The visuals of the wheel</param>
        /// <param name="steeringAngle">The steering angle</param>
        private void UpdateWheelsVisually(WheelCollider wheelCollider, Transform wheelTransform,
            float steeringAngle)
        {
            wheelCollider.GetWorldPose(out var pos,out var wheelColliderRotation);
            var carAngle=transform.localRotation.eulerAngles.y;
            carAngle %=360;
            carAngle= carAngle>180 ? carAngle-360 : carAngle;
            wheelTransform.rotation = Quaternion.Euler(wheelColliderRotation.eulerAngles.x,steeringAngle+carAngle, -90);
        }

        /// <summary>
        /// Start Acceleration; it is a helper method to be called each time we get data from autonomous system
        /// </summary>
        /// <param name="control">The control result from the as;i.e the values that we get from automation system</param>
        private void Acceleration(ControlResultMessage control)
        {
            var steeringAngleInDegrees = control.steering_angle_target * Mathf.Rad2Deg;

            //steering
            frontLeftCollider.steerAngle = steeringAngleInDegrees;
            frontRightCollider.steerAngle = steeringAngleInDegrees;
            // Autonomous system moves the car
                if (control.speed_target != 0f)
                {   
                    resetBrakes();
                    rearLeftCollider.motorTorque =_pidOutput*50;
                    rearRightCollider.motorTorque =_pidOutput*50;
                    
                }
                else
                {
                    Declaration();
                }
                UpdateWheelsVisually(frontLeftCollider, frontLeftWheel, steeringAngleInDegrees);
            UpdateWheelsVisually(frontRightCollider, frontRightWheel, steeringAngleInDegrees);
            UpdateWheelsVisually(rearRightCollider, rearRightWheel, 0);
            UpdateWheelsVisually(rearLeftCollider, rearLeftWheel, 0);
        }

        private void MoveCarUsingInput()
        {
            //steering
            frontLeftCollider.steerAngle = 35.0f * _moveDirection.x;
            frontRightCollider.steerAngle = 35.0f * _moveDirection.x;

            //breaking
            if (_braking)
            {
                brake();
            }
            else             //Not breaking
            {
                resetBrakes();
                //move forward
                rearLeftCollider.motorTorque = 10.0f * _moveDirection.y;
                rearRightCollider.motorTorque = 10.0f * _moveDirection.y; 
            }

            //steering visually
            UpdateWheelsVisually(frontLeftCollider, frontLeftWheel, frontLeftCollider.steerAngle);
            UpdateWheelsVisually(frontRightCollider, frontRightWheel, frontRightCollider.steerAngle);
            UpdateWheelsVisually(rearRightCollider, rearRightWheel, 0);
            UpdateWheelsVisually(rearLeftCollider, rearLeftWheel, 0);

        }

        /// <summary>
        /// Start Declaration when the motor torque is 0
        /// </summary>
        /// <param name="control">The control result from the as;i.e the values that we get from automation system</param>
        private void Declaration()
        {
            var deceletaionForce =
                10f; // change this if you want the car to decelerate faster; the higher the force the faster the declaration
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

        public void onReset(InputAction.CallbackContext context)
        {
            ResetCar();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            if(!_automated)
                _moveDirection = context.ReadValue<Vector2>() * new Vector2(1, 10);
        }

        public void onBrake(InputAction.CallbackContext context)
        {
            _braking =context.ReadValueAsButton();
            
        }

        private void brake()
        {
            const int brakeTorque = 1000;
            rearLeftCollider.brakeTorque = brakeTorque * 50;
            rearRightCollider.brakeTorque = brakeTorque * 50;
            frontLeftCollider.brakeTorque = brakeTorque * 50;
            frontRightCollider.brakeTorque = brakeTorque * 50;
        }

        private void resetBrakes()
        {
            rearLeftCollider.brakeTorque = 0;
            rearRightCollider.brakeTorque = 0;
            frontLeftCollider.brakeTorque = 0;
            frontRightCollider.brakeTorque = 0;
        }
    }
}