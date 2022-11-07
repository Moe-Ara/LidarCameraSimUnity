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
using System.Threading;

namespace DefaultNamespace
{
    public class CommunicationInt : MonoBehaviour
    {
        private bool isASConnected;
        
        #region objects

        //Sensors to get data from
        private GssController _gssController;
        private Imu_Controller _imuController;
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
        //[SerializeField] public TextMeshProUGUI GSS;
        //[SerializeField] public TextMeshProUGUI IMU;
        [SerializeField] public TMP_InputField GSSXInput;
        [SerializeField] public TMP_InputField GSSYInput;
        [SerializeField] public TMP_InputField GSSZInput;
        [SerializeField] public TMP_InputField IMUXInput;
        [SerializeField] public TMP_InputField IMUYInput;
        [SerializeField] public TMP_InputField IMUZInput;
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
        [SerializeField] public TextMeshProUGUI ASConnection;

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
            GSSXInput.text = Math.Round(_gssController.Velocity.x, 2).ToString("0.00");
            GSSYInput.text = Math.Round(_gssController.Velocity.y, 2).ToString("0.00");
            GSSZInput.text = Math.Round(_gssController.Velocity.z, 2).ToString("0.00");
            IMUXInput.text = Math.Round(_imuController.Acceleration.x, 2).ToString("0.00");
            IMUYInput.text = Math.Round(_imuController.Acceleration.y, 2).ToString("0.00");
            IMUZInput.text = Math.Round(_imuController.Acceleration.z, 2).ToString("0.00");
            /*GSS.SetText(string.Format("GSS x: {0} y: {1} z: {2}", Math.Round(_gssController.velocity.x, 2),
                Math.Round(_gssController.velocity.y, 2), Math.Round(_gssController.velocity.z, 2)));
            IMU.SetText(string.Format("IMU  x: {0} y: {1} z: {2}", Math.Round(_imuController.acceleration.x, 2),
                Math.Round(_imuController.acceleration.y, 2), Math.Round(_imuController.acceleration.z, 2)));*/
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
                if (Math.Abs(_cameraHz - _cameraController.Hz) > 0.0001) 
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
                if (Math.Abs(_GSSError - _gssController.ErrorRate) > 0.0001)
                {
                    _gssController.ErrorRate = (float)_GSSError;
                    GSSErrorInput.text = _gssController.ErrorRate.ToString("0.00");
                }
            }
            if (GSSSamplingInput.text != null)
            {
                _GSSSampling = float.Parse(GSSSamplingInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_GSSSampling - _gssController.SamplingRate) > 0.0001) 
                {                
                    _gssController.SamplingRate = (float)_GSSSampling;
                    GSSSamplingInput.text = _gssController.SamplingRate.ToString("0.00");
                }
            }
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
                if (Math.Abs(_IMUError - _imuController.ErrorRate) > 0.0001)
                {
                    _imuController.ErrorRate = (float)_IMUError;
                    IMUErrorInput.text = _imuController.ErrorRate.ToString("0.00");
                }
            }
            // missing IMUSampling 
            if (IMUSamplingInput.text != null)
            {
                _IMUSampling = float.Parse(IMUSamplingInput.text, CultureInfo.InvariantCulture.NumberFormat);
                if (Math.Abs(_IMUSampling - _imuController.SamplingRate) > 0.0001) 
                {                
                    _imuController.SamplingRate = (float)_IMUSampling;
                    IMUSamplingInput.text = _imuController.SamplingRate.ToString("0.00");
                }
            }
            
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
            _GSSError = _gssController.ErrorRate;
            GSSErrorInput.text = _gssController.ErrorRate.ToString("0.00");
            _GSSSampling = _gssController.SamplingRate;
            GSSSamplingInput.text = _gssController.SamplingRate.ToString("0.00");
            _IMUOffset = _imuController.IMUOffset;
            IMUOffsetInput.text = _imuController.IMUOffset.ToString("0.00");
            _IMUError = _imuController.ErrorRate;
            IMUErrorInput.text = _imuController.ErrorRate.ToString("0.00");
            _IMUSampling = _imuController.SamplingRate;
            IMUSamplingInput.text = _imuController.SamplingRate.ToString("0.00");
        }

        public void UpdateInfo()
        {
            if (isASConnected == true)
            {
                ASConnection.SetText("Autonomous system: connected");
                ASConnection.color = Color.green;
            }
            else
            {
                ASConnection.SetText("Autonomous system:\nnot connected");
                ASConnection.color = Color.red;
            }
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
                <Imu_Controller>();
            _carRigidBody = car.GetComponent
                <Rigidbody>();
            _Mass = _carRigidBody.mass;
            _lidarHz = _lidarController.Hz;
            _cameraHz = _cameraController.Hz;
            // missing friction initialization
            _GSSOffset = _gssController.GSSoffset;
            _GSSError = _gssController.ErrorRate;
            _GSSSampling = _gssController.SamplingRate;
            _IMUOffset = _imuController.IMUOffset;
            _IMUError = _imuController.ErrorRate;
            // missing IMUSampling initialization
            massInput.text = _carRigidBody.mass.ToString("0.00");
            lidarInput.text = _lidarController.Hz.ToString("0.00");
            cameraInput.text = _cameraController.Hz.ToString("0.00");
            frictionInput.text = "0"; // missing friction declaration
            GSSOffsetInput.text = _gssController.GSSoffset.ToString("0.00");
            GSSErrorInput.text = _gssController.ErrorRate.ToString("0.00");
            GSSSamplingInput.text = _gssController.SamplingRate.ToString("0.00"); 
            IMUOffsetInput.text = _imuController.IMUOffset.ToString("0.00");
            IMUErrorInput.text = _imuController.ErrorRate.ToString("0.00");
            IMUSamplingInput.text = "0"; // missing IMUSampling declaration
            ASConnection.SetText("Autonomous system: ");
        }

        void Update()
        {
            _Velocity = _gssController.Velocity;
            _Speed = _gssController.Speed;
            _AccelerationVector = _imuController.Acceleration;
            _Acceleration = _AccelerationVector.magnitude;
            Display();
            UpdateInfo();
        }
    }
}