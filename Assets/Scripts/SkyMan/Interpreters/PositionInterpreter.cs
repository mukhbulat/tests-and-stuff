using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyMan.Interpreters
{
    public class PositionInterpreter : IPositionInterpreter
    {
        public event Action NewPositionAdded;
        public Vector3 CurrentTargetPosition => _positionBuffer.Peek();
        
        private Queue<Vector3> _positionBuffer = new Queue<Vector3>();

        public void AddNewPosition(Vector3 position)
        {
            _positionBuffer.Enqueue(position);
            NewPositionAdded?.Invoke();
        }

        public bool RemoveOldPosition(out Vector3 oldPosition)
        {
            if (_positionBuffer.Count == 0)
            {
                oldPosition = new Vector3();
                return false;
            }
            else
            {
                oldPosition = _positionBuffer.Dequeue();
                return true;
            }
        }
    }
}