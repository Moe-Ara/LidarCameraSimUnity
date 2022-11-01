using System;
using System.Collections;
using System.Numerics;
using Car.Camera;
using Car.gss;
using Car.imu;
using Car.Lidar;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using TMPro;

namespace DefaultNamespace
{
    public class communucationInt : MonoBehaviour
    {
        #region objects

//Sensors to get data from
        private GssController _gssController;
        private imuCont _imuController;
        private CarCameraController _cameraController;
        private LidarController _lidarController;

        #endregion

        #region serializableObjects

        [SerializeField] private GameObject _car;

        [SerializeField] private GameObject _lidar;
        [SerializeField] private GameObject _camera;
        [SerializeField] private GameObject _imu;
        [SerializeField] private GameObject _gss;
        [SerializeField] public TextMeshProUGUI mass;
        [SerializeField] public TextMeshProUGUI speed;
        [SerializeField] public TextMeshProUGUI GSS;
        [SerializeField] public TextMeshProUGUI IMU;

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
            mass.SetText($"mass   " + _Mass + " kg");
            speed.SetText($"speed  " + Math.Round(_Speed, 2) + " km/h");
            GSS.SetText(string.Format("GSS x: {0} y: {1} z: {2}", Math.Round(_gssController.velocity.x, 2),
                Math.Round(_gssController.velocity.y, 2), Math.Round(_gssController.velocity.z, 2)));
            IMU.SetText(string.Format("IMU  x: {0} y: {1} z: {2}", Math.Round(_imuController.acceleration.x, 2),
                Math.Round(_imuController.acceleration.y, 2), Math.Round(_imuController.acceleration.z, 2)));
        }

        private void Start()
        {
            _lidarController = _lidar.GetComponent
                <LidarController>();
            _cameraController = _car.GetComponent
                <CarCameraController>();
            _gssController = _gss.GetComponent
                <GssController>();
            _imuController = _imu.GetComponent
                <imuCont>();
            _carRigidBody = _car.GetComponent
                <Rigidbody>();
            _Mass = _carRigidBody.mass;
            _lidarHz = _lidarController.Hz;
            _cameraHz = _cameraController.Hz;
        }

        void Update()
        {
            _Velocity = _gssController.velocity;
            _Speed = _gssController.Speed;
            _AccelerationVector = _imuController.acceleration;
            _Acceleration = _AccelerationVector.magnitude;


            Display();

            //edit
            if (Math.Abs(_Mass - _carRigidBody.mass) > 0.0001)
            {
                _carRigidBody.mass = (float)_Mass;
            }

            if (Math.Abs(_lidarHz - _lidarController.Hz) > 0.0001)
            {
                _lidarController.Hz = (float)_lidarHz;
            }

            _cameraController.Hz = (_cameraHz - _cameraController.Hz) > 0.0001 ? _cameraHz : _cameraController.Hz;

        }
    }
}