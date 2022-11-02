using System;
using System.Collections;
using System.Numerics;
using Car.Camera;
using Car.gss;
using Car.imu;
using Car.Lidar;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using TMPro;
using System.Globalization;

namespace DefaultNamespace
{
    public class CommunicationInt : MonoBehaviour
    {
        #region objects

        //Sensors to get data from
        private GssController _gssController;
        private imuCont _imuController;
        private CarCameraController _cameraController;
        private LidarController _lidarController;

        #endregion

        #region serializableObjects

        [SerializeField] private GameObject car;
        [SerializeField] private GameObject lidar;
        [SerializeField] private GameObject camera;
        [SerializeField] private GameObject imu;
        [SerializeField] private GameObject gss;
        [SerializeField] public TextMeshProUGUI mass;
        [SerializeField] public TextMeshProUGUI speed;
        [SerializeField] public TextMeshProUGUI GSS;
        [SerializeField] public TextMeshProUGUI IMU;
        [SerializeField] public TMP_InputField massInput;
        [SerializeField] public TMP_InputField lidarInput;
        [SerializeField] public TMP_InputField cameraInput;
        [SerializeField] public TMP_InputField frictionInput;
        [SerializeField] public TMP_InputField GSSOffsetInput;
        [SerializeField] public TMP_InputField GSSErrorInput;
        [SerializeField] public TMP_InputField GSSSamplingInput;
        [SerializeField] public TMP_InputField IMUOffsetInput;
        [SerializeField] public TMP_InputField IMUErrorInput;
        [SerializeField] public TMP_InputField IMUSamplingInput;
        
        #endregion

        //car rigid body to get mass
        private Rigidbody _carRigidBody;

        #region Variables
        private Vector3 _Velocity;
        private double _Speed;
        private double _Acceleration; //maybe think about that later
        private Vector3 _AccelerationVector;
        #endregion

        #region Changables
        private float _Mass;
        private float _lidarHz;
        private float _cameraHz;
        #endregion

        void Display()
        {
            mass.SetText($"mass   " + _carRigidBody.mass + " kg");
            speed.SetText($"speed  " + Math.Round(_gssController.Speed, 2) + " km/h");
            GSS.SetText(string.Format("GSS x: {0} y: {1} z: {2}", Math.Round(_gssController.velocity.x, 2),
                Math.Round(_gssController.velocity.y, 2), Math.Round(_gssController.velocity.z, 2)));
            IMU.SetText(string.Format("IMU  x: {0} y: {1} z: {2}", Math.Round(_imuController.acceleration.x, 2),
                Math.Round(_imuController.acceleration.y, 2), Math.Round(_imuController.acceleration.z, 2)));
        }
        
        public void SaveInput()
        {
            if (massInput.text != null)
            {
                _Mass = float.Parse(massInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_Mass - _carRigidBody.mass) > 0.0001)
                {
                    _carRigidBody.mass = (float)_Mass;
                }
            }
            if (lidarInput.text != null)
            {
                _lidarHz = float.Parse(lidarInput.text, CultureInfo.InvariantCulture.NumberFormat); 
                if (Math.Abs(_lidarHz - _lidarController.Hz) > 0.0001)
                {
                    _lidarController.Hz = (float)_lidarHz;
                }
            }
            if (cameraInput.text != null)
            {
                _cameraHz = float.Parse(cameraInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_cameraHz - _cameraController.Hz) > 0.0001) // _cameraController.Hz = (_cameraHz - _cameraController.Hz) > 0.0001 ? _cameraHz : _cameraController.Hz;
                {
                    _cameraController.Hz = (float)_cameraHz;
                }
            }
            /*
            if (frictionInput.text != null)
            {
                _friction = float.Parse(frictionInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_friction - _cameraController.friction) > 0.0001) 
                {                {
                    _cameraController.friction = (float)_friction;
                }
            }
            if (GSSOffsetInput.text != null)
            {
                _GSSOffset = float.Parse(GSSOffsetInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_GSSOffset - _cameraController.GSSOffset) > 0.0001) 
                {                {
                    _cameraController.GSSOffset = (float)_GSSOffset;
                }
            }
            if (GSSErrorInput.text != null)
            {
                _GSSError = float.Parse(GSSErrorInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_GSSError - _cameraController.GSSError) > 0.0001) 
                {                {
                    _cameraController.GSSError = (float)_GSSError;
                }
            }
            if (GSSSamplingInput.text != null)
            {
                _GSSSampling = float.Parse(GSSSamplingInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_GSSSampling - _cameraController.GSSSampling) > 0.0001) 
                {                {
                    _cameraController.GSSSampling = (float)_GSSSampling;
                }
            }
            if (IMUOffsetInput.text != null)
            {
                _IMUOffset = float.Parse(IMUOffsetInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_IMUOffset - _cameraController.IMUOffset) > 0.0001) 
                {                {
                    _cameraController.IMUOffset = (float)_IMUOffset;
                }
            }
            if (IMUErrorInput.text != null)
            {
                _IMUError = float.Parse(IMUOffsetInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_IMUError - _cameraController.IMUError) > 0.0001) 
                {                {
                    _cameraController.IMUError = (float)_IMUError;
                }
            }
            if (IMUSamplingInput.text != null)
            {
                _IMUSampling = float.Parse(IMUSamplingInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_IMUSampling - _cameraController.IMUSampling) > 0.0001) 
                {                {
                    _cameraController.IMUSampling = (float)_IMUSampling;
                }
            }
            */
        }

        public void CloseInput()
        {
            _Mass = _carRigidBody.mass;
            massInput.text = _carRigidBody.mass.ToString("0.00");
            _lidarHz = _lidarController.Hz;
            lidarInput.text = _lidarController.Hz.ToString("0.00");
            _cameraHz = _cameraController.Hz;
            cameraInput.text = _cameraController.Hz.ToString("0.00");
            /*
             * _friction = _cameraController.friction;
             * _GSSOffset = _cameraController.GSSOffset;
             * _GSSError = _cameraController.GSSError;
             * _GSSSampling = _cameraController.GSSSampling;
             * _IMUOffset = _cameraController.IMUOffset;
             * _IMUError = _cameraController.IMUError;
             * _IMUSampling = _cameraController.IMUSampling;
            */
            
        }

        private void Start()
        {
            _lidarController = lidar.GetComponent
                <LidarController>();
            _cameraController = car.GetComponent
                <CarCameraController>();
            _gssController = gss.GetComponent
                <GssController>();
            _imuController = imu.GetComponent
                <imuCont>();
            _carRigidBody = car.GetComponent
                <Rigidbody>();
            _Mass = _carRigidBody.mass;
            _lidarHz = _lidarController.Hz;
            _cameraHz = _cameraController.Hz;
            massInput.text = _carRigidBody.mass.ToString("0.00");
            lidarInput.text = "0";
            cameraInput.text = "0";
            frictionInput.text = "0";
            GSSOffsetInput.text = "0";
            GSSErrorInput.text = "0";
            GSSSamplingInput.text = "0";
            IMUOffsetInput.text = "0";
            IMUErrorInput.text = "0";
            IMUSamplingInput.text = "0";
        }

        void Update()
        {
            _Velocity = _gssController.velocity;
            _Speed = _gssController.Speed;
            _AccelerationVector = _imuController.acceleration;
            _Acceleration = _AccelerationVector.magnitude;
            Display();
        }
    }
}