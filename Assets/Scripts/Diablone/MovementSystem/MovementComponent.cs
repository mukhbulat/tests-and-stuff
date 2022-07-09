using System.Collections;
using UnityEngine;

namespace Diablone.MovementSystem
{
    public class MovementComponent : MonoBehaviour, IMovable
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _minDistance = 0.12f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _gravity = 10f;
        [SerializeField] private AudioSource _footstepAudio;
        [SerializeField] private float _pitchLowest = 2.9f;
        [SerializeField] private float _pitchHighest = 3.2f;
        
        private Coroutine _moving;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

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
            while ((transform.position - target).magnitude > _minDistance)
            {
                var velocity = (target - transform.position).normalized * _moveSpeed;
                
                RotationHandle(velocity);
                
                VerticalMovement(ref velocity);
                _characterController.Move(velocity * Time.deltaTime);
                
                AudioHandle(true);
                AnimatorHandle(true);
                
                yield return null;
            }
            
            AudioHandle(false);
            AnimatorHandle(false);
        }

        private void RotationHandle(Vector3 velocity)
        {
            velocity.y = transform.parent.position.y;
            transform.parent.LookAt(velocity);
        }

        private void AudioHandle(bool isMoving)
        {
            if (!isMoving)
            {
                _footstepAudio.Stop();
                return;
            }
            if (_footstepAudio.isPlaying) return;
            
            _footstepAudio.pitch = Random.Range(_pitchLowest, _pitchHighest);
            _footstepAudio.Play();
        }

        private void AnimatorHandle(bool isMoving)
        {
            _animator.SetBool(IsMoving, isMoving);
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
                AnimatorHandle(false);
            }
        }

        public void ChangeMoveSpeed(float addition)
        {
            _moveSpeed += addition;
        }
    }
}