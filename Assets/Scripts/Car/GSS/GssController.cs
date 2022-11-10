using System;
using System.Collections;
using System.Collections.Generic;
using Communication.Messages;
using UnityEngine;
using UnityEngine.UIElements;

namespace Car.gss
{
    public class GssController : MonoBehaviour
    {
        #region Attributes

        private Vector3 _lastPositon;
        [SerializeField] private float _speed;
        [SerializeField] private double _speedKmH;
        private float _offset;
        private Vector3 _velocity;
        private float _errorRate;
        private float _samplingRateInKHZ;
        private DateTime _last;
        private float _samplingRateInHZ;

        #endregion

        #region GettersAndSetters

        public float Speed
        {
            get => _speed;
        }

        public float GSSoffset
        {
            get => _offset;
            set => _offset = value;
        }

        public double SpeedKmH
        {
            get => _speedKmH;
            set => _speedKmH = value;
        }

        public float ErrorRate
        {
            get => _errorRate;
            set => _errorRate = value;
        }

        public float SamplingRate
        {
            get => _samplingRateInKHZ;
            set => _samplingRateInKHZ = value;
        }

        public Vector3 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        #endregion

        // Start is called before the first frame update
        private void Start()
        {
            _last= DateTime.Now;
            _lastPositon = Vector3.zero;
            _velocity = Vector3.zero;
            _errorRate = 0.2f; // in percent
            _samplingRateInKHZ = -1f;
            _speed = 0.0f;
            _speedKmH = 0;
            _offset = 0.0f;
            _samplingRateInHZ = _samplingRateInKHZ * 1000;
        }

        // Update is called once per frame
        private void Update()
        {
            StartCoroutine(nameof(StartCalc));
        }

        /// <summary>
        /// Calculates the velocity and the speed in m/s and kmh. It also adds the error to our speed
        /// </summary>
        private void calculateSpeed()
        {
            var position = transform.position;
            //should update to 1/samplingRate 
            _velocity = (position - _lastPositon) / Time.deltaTime;
            _speed = _velocity.magnitude;
            _lastPositon = position;
            _speedKmH = Math.Round(Math.Abs(_speed * 3.6), 3);
            if (_errorRate > 0)
            {
                if (_speed != 0)
                    _speed = calculateErrorGuassian(_speed, (float)(_errorRate / (100 * 3) * _speed)) * _speed;
            }

            //rounding the speed up to 3 decimal points
            _speed = (float)Math.Round(Math.Abs(_speed + _offset), 3);
        }
        
        /// <summary>
        /// Helper method that calculates a random error based on an error rate
        /// </summary>
        /// <param name="mean">this is the mean of all speeds (we don't use a mean since it isn't really necessary for our case )</param>
        /// <param name="standrad_deviation">the standard deviation</param>
        private float calculateErrorGuassian(float mean, float standrad_deviation)
        {
            var random = new System.Random();
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            var x1 = 1 - (float)random.NextDouble();
            var x2 = 1 - (float)random.NextDouble();
            var y1 = (float)(Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2));
            return (y1 * standrad_deviation + mean) / mean;
        }
        
        /// <summary>
        /// Co-routine that starts the speed calculation
        /// </summary>
        private void StartCalc()
        {
            //check if sampling rate feature is enabled
            if (_samplingRateInKHZ < 0)
            {
                calculateSpeed();
            }
            else
            {
                var now = DateTime.Now;
                if ((now - _last).TotalSeconds < 1f / _samplingRateInHZ) return;
                _last = now;
                calculateSpeed();
            }
        }
    }
}