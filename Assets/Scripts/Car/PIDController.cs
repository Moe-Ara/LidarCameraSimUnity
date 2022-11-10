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

        /// <summary>
        /// Default constructor 
        /// </summary>
        public PIDController() : this(1, 0.1f, 0.1f)
        {
            _errorLast = 0;
            _errorSum = 0;
            _integrationStored = 0;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proportionalGain">proportional gain of the pid controller</param>
        /// <param name="integralGain">integral gain of the pid controller</param>
        /// <param name="derivativeGain">derivative gain of the pid controller</param>
        public PIDController(float proportionalGain, float integralGain, float derivativeGain)
        {
            _proportionalGain = proportionalGain;
            _integralGain = integralGain;
            _derivativeGain = derivativeGain;
            _errorLast = 0;
            _errorSum = 0;
            _integrationStored = 0;
        }
        /// <summary>
        /// Method to calculate the error based on the PID gains
        /// </summary>
        /// <param name="deltaTime">delta time</param>
        /// <param name="currentValue">current value</param>
        /// <param name="targetValue">target value</param>
        /// <returns></returns>
        public float calcPID(float deltaTime, float currentValue, float targetValue)
        {
            var result = 0f;
            //calculate error
            var error = targetValue - currentValue;
            //Proportional 
            var P = _proportionalGain * error;
            //Derivative 
            var dt = (error - _errorLast) / deltaTime;
            var D= _derivativeGain * dt;
            //Integral
            
            _integrationStored = Mathf.Clamp(_integrationStored + (error * deltaTime), -10, 10); //change min and max depending on the pid controller irl; these are integral saturation
            var I = _integralGain * _integrationStored;
            _errorLast = error;
            result = P + I + D;
            return
                result; //Mathf.Clamp(result,-1,1); //-1 and 1 here are placeholders for minimum output and maximum output; they should be defined by the user
        }
        /// <summary>
        /// Helper method that gets the average for the integral part of the pid
        /// </summary>
        /// <param name="oldAverage"></param>
        /// <param name="valueToAdd"></param>
        /// <param name="count"></param>
        /// <returns>new average</returns>
        /// <example>_errorSum = AddValueToAverage(_errorSum, Time.deltaTime * error, 1000f);</example>
        private static float AddValueToAverage(float oldAverage, float valueToAdd, float count)
        {
            var newAverage = ((oldAverage * count) + valueToAdd) / (count + 1f);
            return newAverage;
        }
        
    }
}