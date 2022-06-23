using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyMan
{
    public interface IPositionInterpreter
    {
        public Vector3 CurrentPosition { get; }
        public void AddNewPosition(Vector3 position);
        public bool RemoveOldPosition();
        // Returns true, if positions remain in buffer.
    }
}