using UnityEngine;
using System;
namespace Car.imu
{
    public class ImuController : MonoBehaviour
    {
        private Vector3 errorAcc=Vector3.zero;
        public float _error;
        private Vector3 _lastPositon = Vector3.zero;
        public float _speed = 0.0f;
        public double _speedKmH = 0;
        public float _offset = 0.0f;
        public double _time = 0;
        public Vector3 velocity = Vector3.zero;
        public Vector3 acceleration=Vector3.zero;
        public Vector3 _lastVelocity=Vector3.zero;
        //angle random walk (ARW) in degrees/sqrt(Hr)
        public float angleRandWalk = 0.0f; 
        //error rate based off sensor
        public float errorRate = 0.1f;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            calculateSpeed();
            calculateAcceleration();
        }

        public float calculateSpeed()
        {
            Vector3 pos = transform.position;
            Vector3 difference = (pos - _lastPositon) / Time.deltaTime;
            _time = Time.deltaTime;
            velocity = difference;
            _speed = difference[2];
            _lastPositon = pos;
            _speedKmH = _speed * 3.6;
            addError();
            return _speed + _offset; // multiply by random for error calculation 
        }

        public void addError()
        {
            //simulate random errors based off data sheet;
            // using a third of the percentage error so as standard deviation so most values will be in +- errorRate %
                _speed = calculateErrorGuassian(_speed, (float)(errorRate/(100 * 3) * _speed));
            }

        public void calculateAcceleration()
            // Calculate = a = ^V/^T
        { 
            //GetComponent<Rigidbody>().
            acceleration.x = (velocity.x - _lastVelocity.x) / Time.deltaTime;
            acceleration.z=(velocity.z - _lastVelocity.z) / Time.deltaTime;
            _lastVelocity = velocity;
            calculateError();
            acceleration = errorAcc;
        }

        public void calculateError()
        {
            //Think About how to make this better 
            
            errorAcc.x = calculateErrorGuassian(acceleration.x, (float)(Math.Sqrt(0.03 * acceleration.x)));
            // errorAcc.y = calculateErrorGuassian(acceleration[1], (float)(Math.Sqrt(0.03 * acceleration[1])));
            errorAcc.z = calculateErrorGuassian(acceleration.z, (float)(Math.Sqrt(0.03 * acceleration.z)));
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