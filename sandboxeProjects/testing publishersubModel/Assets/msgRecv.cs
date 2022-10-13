using System;

namespace com
{   
    [Serializable]
    public class msgRecv
    {
        public float speed = 0;

        public float yaw = 0;
        public override string ToString()
        {
            return "speed is: " + speed + " and yaw is: " + yaw;
        }
    }
}