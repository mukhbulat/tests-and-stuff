using System;
using MovementDiff.Interfaces;
using UnityEngine;

namespace MovementDiff.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class DynamicRigidbodyMovement : MonoBehaviour, IMovable
    {

        [SerializeField] private float speed = 5;
        [SerializeField] private ForceMode forceMode = ForceMode.Acceleration;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 direction)
        {
            Vector3 velocity = new Vector3(direction.x, 0, direction.y).normalized * speed;
            _rigidbody.AddForce(velocity, forceMode);
        }
    }
}