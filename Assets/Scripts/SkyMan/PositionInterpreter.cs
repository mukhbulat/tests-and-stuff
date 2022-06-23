using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyMan
{
    public class PositionInterpreter : IPositionInterpreter
    {
        public Vector3 CurrentPosition => _positionBuffer.Peek();
        
        private Queue<Vector3> _positionBuffer = new Queue<Vector3>();

        public void AddNewPosition(Vector3 position)
        {
            _positionBuffer.Enqueue(position);
        }

        public bool RemoveOldPosition()
        {
            if (_positionBuffer.Count == 0)
            {
                return false;
            }
            _positionBuffer.Dequeue();
            return _positionBuffer.Count != 0;
        }
    }
}