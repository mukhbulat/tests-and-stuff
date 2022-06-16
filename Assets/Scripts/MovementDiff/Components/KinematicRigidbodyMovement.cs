using System;
using MovementDiff.Interfaces;
using UnityEditor.UIElements;
using UnityEngine;

namespace MovementDiff.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicRigidbodyMovement : MonoBehaviour, IHorizontalMovable
    {
        [SerializeField] private float speed = 5;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }


        public void Move(Vector2 direction)
        {
            Vector3 velocity = new Vector3(direction.x, 0, direction.y).normalized * speed * Time.fixedDeltaTime;
            velocity += transform.position;
            _rigidbody.MovePosition(velocity);
        }
    }
}