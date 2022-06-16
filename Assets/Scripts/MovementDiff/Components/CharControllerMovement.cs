using System;
using MovementDiff.Interfaces;
using UnityEngine;

namespace MovementDiff.Components
{
    [RequireComponent(typeof(CharacterController))]
    public class CharControllerMovement : MonoBehaviour, IHorizontalMovable, IVerticalMovable
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float gravity = 10;
        [SerializeField] private float jumpHeight = 2;
        [SerializeField] private bool grounded;
        
        private CharacterController _characterController;
        
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
            grounded = _characterController.isGrounded;
            if (grounded)
            {
                _verticalVelocity = -gravity * Time.deltaTime;
            }
            else
            {
                _verticalVelocity -= gravity * Time.deltaTime;
            }
            
            _characterController.Move(_verticalVelocity * Vector3.up * Time.deltaTime);
        }

        public void VerticalMove(float verticalNormalizedVelocity)
        {
            // for CC version there will be only a jump thing.
            if (verticalNormalizedVelocity <= 0) return;
            if (grounded)
            {
                _verticalVelocity += Mathf.Sqrt(jumpHeight * gravity);
            }
        }
    }
}