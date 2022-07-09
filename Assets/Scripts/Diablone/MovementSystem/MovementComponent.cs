using System.Collections;
using UnityEngine;

namespace Diablone.MovementSystem
{
    public class MovementComponent : MonoBehaviour, IMovable
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _gravity = 10f;
        private Coroutine _moving;

        public float MoveSpeed => _moveSpeed;

        public void Move(Vector3 point)
        {
            if (_moving != null)
            {
                StopCoroutine(_moving);
            }

            _moving = StartCoroutine(MovingToTarget(point));
        }

        private IEnumerator MovingToTarget(Vector3 target)
        {
            while ((transform.position - target).magnitude > 0.1f)
            {
                var velocity = (target - transform.position).normalized * _moveSpeed;
                VerticalMovement(ref velocity);
                _characterController.Move(velocity);
                yield return null;
            }
        }

        private void VerticalMovement(ref Vector3 velocity)
        {
            if (_characterController.isGrounded)
            {
                velocity.y = -_gravity * Time.deltaTime;
            }
            else
            {
                velocity.y -= _gravity * Time.deltaTime;
            }
            
        }

        public void Stop()
        {
            if (_moving != null)
            {
                StopCoroutine(_moving);
            }
        }

        public void ChangeMoveSpeed(float addition)
        {
            _moveSpeed += addition;
        }
    }
}