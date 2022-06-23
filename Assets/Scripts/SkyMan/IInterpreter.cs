using UnityEngine;

namespace SkyMan
{
    public interface IInterpreter
    {
        public void AddNewPosition(Vector3 position);
        public void ChangeSpeed(float newSpeed);
    }
}