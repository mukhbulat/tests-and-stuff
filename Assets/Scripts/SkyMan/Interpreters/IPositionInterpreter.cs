using System;
using UnityEngine;

namespace SkyMan.Interpreters
{
    public interface IPositionInterpreter
    {
        public event Action NewPositionAdded;
        public Vector3 CurrentTargetPosition { get; }
        public void AddNewPosition(Vector3 position);
        public bool RemoveOldPosition(out Vector3 oldPosition);
        // Returns true, if positions remain in buffer.
    }
}