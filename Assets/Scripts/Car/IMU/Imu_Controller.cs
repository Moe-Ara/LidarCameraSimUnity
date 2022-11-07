using UnityEngine;
using System;

namespace Car.imu
{
    public class Imu_Controller : MonoBehaviour
    {
        #region Attributes

        private DateTime _last;
        // private Vector3 _errorAcc;
        private Vector3 _lastPositon;
        private float _offset;
        private Vector3 _velocity;
        [SerializeField]
        private Vector3 _acceleration;

        private Vector3 _lastVelocity;

        private float _angleRandomWalk; // should be in mRad/s
        
        

        //error rate based off sensor
        private float _errorRate;
        private float _samplingRate;

        #endregion
        #region GettersAndSetters

        public float IMUOffset
        {
            get => _offset;
            set => _offset = value;
        }

        public float Offset
        {
            get => _offset;
            set => _offset = value;
        }

        public float ErrorRate
        {
            get => _errorRate;
            set => _errorRate = value;
        }

        // public Vector3 ErrorAcc
        // {
        //     get => _errorAcc;
        //     set => _errorAcc = value;
        // }

        public Vector3 Acceleration
        {
            get => _acceleration;
            set => _acceleration = value;
        }

        public float SamplingRate
        {
            get => _samplingRate;
            set => _samplingRate = value;
        }

        #endregion


        // Start is called before the first frame update
        void Start()
        {
            _samplingRate = 500f;
            // _errorAcc = Vector3.zero;
            _lastPositon = Vector3.zero;
            _offset = 0.0f;
            _velocity = Vector3.zero;
            _acceleration = Vector3.zero;
            _lastVelocity = Vector3.zero;

            _angleRandomWalk = 0.0f;
            //error rate based off sensor
            _errorRate = 0.1f; // in percent
            _angleRandomWalk = 0.0f; // rads per second
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            StartCoroutine(nameof(OnCalc));
        }
        
        private void randomWalk()
        {
            //1 per second
            _offset = _offset + _angleRandomWalk;
        }

        private void calculateAcceleration()
            // Calculate = a = ^V/^T
        {
            var pos = transform.position;
            _velocity = (pos - _lastPositon) / Time.deltaTime;
            _lastPositon = pos;
            //GetComponent<Rigidbody>().
            _acceleration.x = (_velocity.x - _lastVelocity.x) / Time.deltaTime;
            _acceleration.y = (_velocity.y - _lastVelocity.y) / Time.deltaTime;
            _acceleration.z = (_velocity.z - _lastVelocity.z) / Time.deltaTime;
            _lastVelocity = _velocity;
            //calculateError();
            if (_acceleration.x != 0)
                _acceleration.x = calculateErrorGuassian(_acceleration.x, ((_errorRate / (100 * 3)) * _acceleration.x)) * _acceleration.x + _offset;
            if (_acceleration.y != 0)
                _acceleration.y = calculateErrorGuassian(_acceleration.y, ((_errorRate / (100 * 3)) * _acceleration.y)) * _acceleration.y + _offset;
            if (_acceleration.z != 0)
                _acceleration.z = calculateErrorGuassian(_acceleration.z, ((_errorRate / (100 * 3)) * _acceleration.z)) * _acceleration.z + _offset;
        }

        private float calculateErrorGuassian(float mean, float stddev)
        {
            var random = new System.Random();
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            var x1 = 1 - (float)random.NextDouble();
            var x2 = 1 - (float)random.NextDouble();
            var y1 = (float)(Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2));
            return (y1 * stddev + mean) / mean;
        }

        private void OnCalc()
        {
            var now = DateTime.Now;
            if ((now - _last).TotalSeconds < 1f / _samplingRate) return;
            _last = now;
            calculateAcceleration();
        }
    }
}