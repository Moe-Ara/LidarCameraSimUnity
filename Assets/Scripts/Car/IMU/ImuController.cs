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
        
        
        
        
        
        /// <summary>
        /// /////////////////////////
        /// </summary>
        /*

         */
        public GameObject car;
        private Rigidbody rb;
        public Vector3 _error;
        private Vector3 _lastPositon = Vector3.zero;
        Vector3 _velocity =Vector3.zero;
        private double _speed=0.0f;
        //ToDo (determine apropriate offset value)
        private double _offset=0.0;
        public Vector3 acceleration = Vector3.zero;
        public Vector3 lastVelocity = Vector3.zero;
        // Start is called before the first frame update
        void Start()
        {
            rb = transform.GetComponent<Rigidbody>();
            
        }

        // Update is called once per frame - 
        void FixedUpdate()
        {
            Debug.Log(rb.velocity);
            calculateVelocity();
            calculateAcceleration();
            if (acceleration[2] >2 )
                Debug.Log(" $$$$ "+ Math.Floor((acceleration[2])) );

        }

        public void calculateVelocity()
        { 
            Vector3 pos = transform.position;
            _velocity = (pos - _lastPositon) / Time.deltaTime;
            _lastPositon = pos;
        }

        public void calculateAcceleration()
        // Calculate = a = ^V/^T
        { 
            //GetComponent<Rigidbody>().
            acceleration.x = (_velocity.x - lastVelocity.x) / Time.fixedDeltaTime;
            acceleration.z=(_velocity.z - lastVelocity.z) / Time.fixedDeltaTime;
            lastVelocity = _velocity;
        }

        public void calculateError()
        {
            _error[0] = calculateErrorGuassian(acceleration[0], (float)(Math.Sqrt(0.03 * acceleration[0])));
            _error[1] = calculateErrorGuassian(acceleration[1], (float)(Math.Sqrt(0.03 * acceleration[1])));
            _error[2] = calculateErrorGuassian(acceleration[2], (float)(Math.Sqrt(0.03 * acceleration[2])));
        }		



    public float calculateErrorGuassian(float mean, float stddev){
			System.Random random=new System.Random();
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).s
            float x1 = 1 - (float)random.NextDouble();
            float x2 = 1 - (float)random.NextDouble();

            float y1 = (float)(Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2));
            return y1 * stddev + mean;
}

    }

}
