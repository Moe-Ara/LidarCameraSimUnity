using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace DefaultNamespace
{
    public class comInt
    {
        private static comInt ComInterfaceInstance= new comInt();
        private ArrayList DispObjects;

        private comInt()
        {
            DispObjects=  new ArrayList();
        }

        public static comInt InitializeComInterface()
        {
            return ComInterfaceInstance ?? new comInt();
        }

        public void DisplayVars()
        {
            //TODO: IMPLEMENT THIS
            throw new NotImplementedException();
        }

        public ArrayList Objects
        {
            get => DispObjects;
            set => DispObjects = value;
        }
    }
}