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
        private Vector3 _lastPositon= Vector3.zero;
        private float _speed=0.0f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {

        }

        public float calculateSpeed()
        {
            Vector3 pos = transform.position;
            Vector3 difference = (pos - _lastPositon) / Time.deltaTime;
            _speed = difference.magnitude;
            _lastPositon = pos;
            return _speed;
        }

        public float calculateError(ControlResultMessage msg)
        {
            _error = (Math.Abs((msg.speed_target - _speed)) / _speed) * 100;
            return _error;
        }
    }

}
