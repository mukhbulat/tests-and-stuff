using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyMan
{
    public class Interpreter : IInterpreter
    {
        private Queue<Vector3> _positionBuffer = new Queue<Vector3>();

        public void AddNewPosition(Vector3 position)
        {
            throw new NotImplementedException();
        }

        public void ChangeSpeed(float newSpeed)
        {
            throw new NotImplementedException();
        }
    }
}