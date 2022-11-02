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
        private float _GSSOffset;
        private float _GSSError;
        private float _GSSSampling;
        private float _IMUOffset;
        private float _IMUError;
        private float _IMUSampling;
        #endregion

        void Display()
        {
            mass.SetText($"mass   " + _carRigidBody.mass.ToString("0.00") + " kg");
            speed.SetText($"speed  " + Math.Round(_gssController.Speed, 2).ToString("0.00") + " km/h");
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
                    massInput.text = _Mass.ToString("0.00");
                }
            }

            if (lidarInput.text != null)
            {
                _lidarHz = float.Parse(lidarInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_lidarHz - _lidarController.Hz) > 0.0001)
                {
                    _lidarController.Hz = (float)_lidarHz;
                    lidarInput.text = _lidarHz.ToString("0.00");
                }
            }

            if (cameraInput.text != null)
            {
                _cameraHz = float.Parse(cameraInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_cameraHz - _cameraController.Hz) >
                    0.0001) // _cameraController.Hz = (_cameraHz - _cameraController.Hz) > 0.0001 ? _cameraHz : _cameraController.Hz;
                {
                    _cameraController.Hz = (float)_cameraHz;
                    cameraInput.text = _cameraHz.ToString("0.00");
                }
            }
            /* missing friction 
            if (frictionInput.text != null)
            {
                _friction = float.Parse(frictionInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_friction - _cameraController.friction) > 0.0001) 
                {                
                    _cameraController.friction = (float)_friction;
                    frictionInput.text = _friction.ToString("0.00");
                }
            }
            */
            if (GSSOffsetInput.text != null)
            {
                _GSSOffset = float.Parse(GSSOffsetInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_GSSOffset - _gssController.GSSoffset) > 0.0001)
                {
                    _gssController.GSSoffset = (float)_GSSOffset;
                    GSSOffsetInput.text = _gssController.GSSoffset.ToString("0.00");
                }
            }

            if (GSSErrorInput.text != null)
            {
                _GSSError = float.Parse(GSSErrorInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_GSSError - _gssController._error) > 0.0001)
                {
                    _gssController._error = (float)_GSSError;
                    GSSErrorInput.text = _gssController._error.ToString("0.00");
                }
            }

            /* missing GSSSampling 
            if (GSSSamplingInput.text != null)
            {
                _GSSSampling = float.Parse(GSSSamplingInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_GSSSampling - _gssController.GSSSampling) > 0.0001) 
                {                
                    _gssController.GSSSampling = (float)_GSSSampling;
                    GSSSamplingInput.text = _gssController.GSSSampling.ToString("0.00");
                }
            }
            */
            if (IMUOffsetInput.text != null)
            {
                _IMUOffset = float.Parse(IMUOffsetInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_IMUOffset - _imuController.IMUOffset) > 0.0001)
                {
                    _imuController.IMUOffset = (float)_IMUOffset;
                    IMUOffsetInput.text = _imuController.IMUOffset.ToString("0.00");
                }
            }

            if (IMUErrorInput.text != null)
            {
                _IMUError = float.Parse(IMUOffsetInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_IMUError - _imuController._error) > 0.0001)
                {
                    _imuController._error = (float)_IMUError;
                    IMUErrorInput.text = _imuController._error.ToString("0.00");
                }
            }
            /* missing IMUSampling 
            if (IMUSamplingInput.text != null)
            {
                _IMUSampling = float.Parse(IMUSamplingInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_IMUSampling - _imuController.IMUSampling) > 0.0001) 
                {                
                    _imuController.IMUSampling = (float)_IMUSampling;
                    IMUSamplingInput.text = _imuController.IMUSampling.ToString("0.00");
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
            /* missing friction 
            _friction = _cameraController._friction;
            frictionInput.text = _cameraController._friction.ToString("0.00");
            */
            _GSSOffset = _gssController.GSSoffset;
            GSSOffsetInput.text = _gssController.GSSoffset.ToString("0.00");
            _GSSError = _gssController._error;
            GSSErrorInput.text = _gssController._error.ToString("0.00");
            /* missing GSSSampling
            _GSSSampling = _cameraController.GSSSampling;
            GSSSamplingInput.text = _cameraController.GSSSampling.ToString("0.00");
            */
            _IMUOffset = _imuController.IMUOffset;
            IMUOffsetInput.text = _imuController.IMUOffset.ToString("0.00");
            _IMUError = _imuController._error;
            IMUErrorInput.text = _imuController._error.ToString("0.00");
            /* missing IMUSampling
            _IMUSampling = _cameraController.IMUSampling;
            IMUSamplingInput.text = _cameraController.IMUSampling.ToString("0.00");
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
            // missing friction initialization
            _GSSOffset = _gssController.GSSoffset;
            _GSSError = _gssController._error;
            // missing GSSSampling initialization
            _IMUOffset = _imuController.IMUOffset;
            _IMUError = _imuController._error;
            // missing IMUSampling initialization
            massInput.text = _carRigidBody.mass.ToString("0.00");
            lidarInput.text = _lidarController.Hz.ToString("0.00");
            cameraInput.text = _cameraController.Hz.ToString("0.00");
            frictionInput.text = "0"; // missing friction declaration
            GSSOffsetInput.text = _gssController.GSSoffset.ToString("0.00");
            GSSErrorInput.text = _gssController._error.ToString("0.00");
            GSSSamplingInput.text = "0"; // missing GSSSampling declaration
            IMUOffsetInput.text = _imuController.IMUOffset.ToString("0.00");
            IMUErrorInput.text = _imuController._error.ToString("0.00");
            IMUSamplingInput.text = "0"; // missing IMUSampling declaration
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