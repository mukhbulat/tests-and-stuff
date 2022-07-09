using UnityEngine;

namespace Diablone.MovementSystem
{
    public interface IMovable
    {
        public float MoveSpeed { get; }
        public void Move(Vector3 point);
        public void Stop();
        
        public void ChangeMoveSpeed(float addition);
    }
}