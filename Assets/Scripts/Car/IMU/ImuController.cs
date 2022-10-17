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
        public Vector3 acceleration = 0;
        public Vector3 lastVelocity = 0;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame - 
        void FixedUpdate()
        {
            acceleration = calculateAcceleration();

        }

        public double calculateAcceleration()
        // Calculate = a = ^V/^T
        { 
            acceleration = (rigidbody.velocity - lastVelocity) / Time.fixedDeltaTime;
            lastVelocity = rigidbody.velocity;
            return acceleration;
        }

        public double calculateError(ControlResultMessage msg)
        {
            _error = calculateErrorGuassian(acceleration, (float)(Math.Sqrt(0.03 * acceleration)));
            return _error;
        }		



    public float calculateErrorGuassian(float mean, float stddev){
			System.Random random=new System.Random();
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            float x1 = 1 - (float)random.NextDouble();
            float x2 = 1 - (float)random.NextDouble();

            float y1 = (float)(Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2));
            return y1 * stddev + mean;
}

    }

}
