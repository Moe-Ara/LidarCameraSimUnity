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
        [SerializeField]
        private float _speed;
        [SerializeField]
        private double _speedKmH;
        private float _offset;
        private Vector3 _velocity;
        private float _errorRate;
        private float _samplingRate;
        private DateTime _last;

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
            get => _samplingRate;
            set => _samplingRate = value;
        }

        public Vector3 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            _lastPositon = Vector3.zero;
            _velocity= Vector3.zero;
            _errorRate = 0.2f;
            _samplingRate = 500f;
            _speed = 0.0f;
            _speedKmH = 0;
            _offset = 0.0f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            StartCoroutine(nameof(OnCalc));
        }

        private void calculateSpeed()
        {
            Vector3 pos = transform.position;
            //should update to 1/samplingRate 
            _velocity = (pos - _lastPositon) / Time.deltaTime;
            _speed = _velocity[2];
            _lastPositon = pos;
            _speedKmH = Math.Round(Math.Abs(_speed * 3.6), 3);
            addError();
            _speed = (float)Math.Round(Math.Abs(_speed + _offset), 3); // multiply by random for error calculation 
        }

        private void addError()
        {
            //simulate random errors based off data sheet;
            // using a third of the percentage error so as standard deviation so most values will be in +- errorRate %
            _speed = calculateErrorGuassian(_speed, (float)(_errorRate / (100 * 3) * _speed));
        }

        private float calculateErrorGuassian(float mean, float stddev)
        {
            System.Random random = new System.Random();
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            var x1 = 1 - (float)random.NextDouble();
            var x2 = 1 - (float)random.NextDouble();
            var y1 = (float)(Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2));
            return y1 * stddev + mean;
        }

        private void OnCalc()
        {
            var now = DateTime.Now;
            if ((now - _last).TotalSeconds < 1f / _samplingRate) return;
            _last = now;
            calculateSpeed();
        }
    }
}