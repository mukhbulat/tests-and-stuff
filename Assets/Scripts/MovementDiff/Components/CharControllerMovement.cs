using System;
using MovementDiff.Interfaces;
using UnityEngine;

namespace MovementDiff.Components
{
    [RequireComponent(typeof(CharacterController))]
    public class CharControllerMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float gravity = 10;
        
        private CharacterController _characterController;
        private bool _grounded;
        private float _verticalVelocity;
        

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector2 direction)
        {
            Vector3 velocity = new Vector3(direction.x, 0, direction.y).normalized * speed;
            
            _characterController.Move(velocity * Time.deltaTime);
        }

        private void Update()
        {
            _grounded = _characterController.isGrounded;
            if (_grounded)
            {
                _verticalVelocity = -gravity * Time.deltaTime;
            }
            else
            {
                _verticalVelocity -= gravity * Time.deltaTime;
            }
            
            _characterController.Move(_verticalVelocity * Vector3.up * Time.deltaTime);
        }
    }
}