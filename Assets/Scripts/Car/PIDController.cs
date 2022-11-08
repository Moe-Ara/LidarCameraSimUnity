using System;
using System.Threading;
using UnityEngine;
namespace Car
{
    public class PIDController: MonoBehaviour
    {
        [SerializeField]
        [Range(-10,10)]
        public float _proportionalGain,_integralGain,_derivativeGain;
        private float _errorLast;
        private float _errorSum;
        private float _integrationStored;
        //Sometimes you have to limit the total sum of all errors used in the I
        private float _errorMax = 20f;

        public PIDController() : this(1, 0.1f, 0.1f)
        {
            _errorLast = 0;
            _errorSum = 0;
            _integrationStored = 0;
        }
        public PIDController(float proportionalGain, float integralGain, float derivativeGain)
        {
            _proportionalGain = proportionalGain;
            _integralGain = integralGain;
            _derivativeGain = derivativeGain;
            _errorLast = 0;
            _errorSum = 0;
            _integrationStored = 0;
        }

        public float calcPID(float deltaTime, float currentValue, float targetValue)
        {
            var result = 0f;
            //caclulate error
            var error = targetValue - currentValue;
            //Proportional 
            var P = _proportionalGain * error;
            //Derivative 
            var dt = (error - _errorLast) / deltaTime;
            var D= _derivativeGain * dt;
            //Integral
            // _errorSum = AddValueToAverage(_errorSum, Time.deltaTime * error, 1000f);
            _integrationStored = Mathf.Clamp(_integrationStored + (error * deltaTime), -10, 10); //change min and max depending on the pid controller irl; these are integral saturation
            var I = _integralGain * _integrationStored;
            _errorLast = error;
            result = P + I + D;
            return
                result; //Mathf.Clamp(result,-1,1); //-1 and 1 here are placeholders for minimum output and maximum output; they should be defined by the user
        }
        private static float AddValueToAverage(float oldAverage, float valueToAdd, float count)
        {
            var newAverage = ((oldAverage * count) + valueToAdd) / (count + 1f);
            return newAverage;
        }
        
    }
}