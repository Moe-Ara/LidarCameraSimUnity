using UnityEngine;
using System;
namespace Car.imu
{
    public class imuCont : MonoBehaviour
    {
        private Vector3 errorAcc=Vector3.zero;
        public float _error;
        private Vector3 _lastPositon = Vector3.zero;
        public float _speed = 0.0f;
        public double _speedKmH = 0;
        private float _offset = 0.0f;
        public double _time = 0;
        public Vector3 velocity = Vector3.zero;
        public Vector3 acceleration=Vector3.zero;
        public Vector3 _lastVelocity=Vector3.zero;
        
        public float IMUOffset
        {
            get => _offset;
            set => _offset = value;
        }
        
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