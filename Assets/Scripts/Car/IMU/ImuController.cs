using System;
using System.Collections;
using System.Collections.Generic;
using Communication.Messages;
using UnityEngine;
using UnityEngine.UIElements;

namespace Car.imu
{
    public class ImuController : MonoBehaviour
    {
        /*

         */
        public double _error;
        private double _lastSpeed = 0;
        private double _speed=0.0f;
        //ToDo (determine apropriate offset value)
        private double _offset=0.0;
        public GssController GSS;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _speed = GSS._speed;
        }

        public double calculateAcceleration()
        // Calculate = a = ^V/^T
        { 
            return (_speed - _lastSpeed)/Time.deltaTime;
        }

        public double calculateError(ControlResultMessage msg)
        {
            _error = (Math.Abs((msg.speed_target - _speed)) / _speed) * 100;
            return _error;
        }
    }

}
