using System.Collections;
using UnityEngine;

namespace Diablone.MovementSystem
{
    public class MovementComponent : MonoBehaviour, IMovable
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _gravity = 10f;
        [SerializeField] private AudioSource _footstepAudio;
        [SerializeField] private float _pitchLowest = 0.9f;
        [SerializeField] private float _pitchHighest = 1.2f;
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
            while ((transform.position - target).magnitude > 0.1f)
            {
                var velocity = (target - transform.position).normalized * _moveSpeed;
                VerticalMovement(ref velocity);
                _characterController.Move(velocity);
                
                AudioWork();
                AnimatorWork(true);
                
                yield return null;
            }
            
            AnimatorWork(false);
        }

        private void AudioWork()
        {
            if (_footstepAudio.isPlaying) return;
            
            _footstepAudio.pitch = Random.Range(_pitchLowest, _pitchHighest);
            _footstepAudio.Play();
        }

        private void AnimatorWork(bool isMoving)
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
                AnimatorWork(false);
            }
        }

        public void ChangeMoveSpeed(float addition)
        {
            _moveSpeed += addition;
        }
    }
}