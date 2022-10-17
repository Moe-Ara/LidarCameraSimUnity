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
        /*
         * relative_freqency= (velocity/propagation_speed_of_waves)*initial_frequency
         * 
         * https://www.parker.com/literature/Electronic%20Controls%20Division/Literature%20files/TGSS_740_instruction_book.pdf
         * 
         * https://en.wikipedia.org/wiki/Radar_speed_gun
         */
        public float _error;
        private Vector3 _lastPositon = Vector3.zero;
        public float _speed = 0.0f;
        public double _speedKmH = 0;
        private float _offset = 0.0f;

        public double _time = 0;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            calculateSpeed();
        }

        public float calculateSpeed()
        {
            Vector3 pos = transform.position;
            Vector3 difference = (pos - _lastPositon) / Time.deltaTime;
            _time = Time.deltaTime;
            _speed = difference.magnitude;
            _lastPositon = pos;
            _speedKmH = _speed * 3.6;
            addError();
            return _speed + _offset; // multiply by random for error calculation 
        }

        public void addError()
        {
            if (_speed < 3.5f)
            {
                _speed = calculateErrorGuassian(_speed, (float)(Math.Sqrt(0.03 * _speed)));
            }
            else if (_speed > 3.5)
            {
                _speed = calculateErrorGuassian(_speed, (float)(Math.Sqrt(0.01 * _speed)));
            }
            //_speed= _speed+_error;
        }

        public float calculateErrorGuassian(float mean, float stddev)
        {
            System.Random random = new System.Random();
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            float x1 = 1 - (float)random.NextDouble();
            float x2 = 1 - (float)random.NextDouble();

            float y1 = (float)(Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2));
            return y1 * stddev + mean;
        }
        
    }
}