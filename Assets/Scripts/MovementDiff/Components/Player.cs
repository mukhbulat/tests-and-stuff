using System.Collections;
using System.Collections.Generic;
using MovementDiff.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementDiff.Components
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        // I can't serialize interface field in inspector, so there are some ways - just get every IMovable, use
        // implementing components instead of interfaces, add scriptable object with all movables
        // that would be interacted(same as using components, not interfaces) and search
        // by tag/gameObject. Also I can use abstract class inheriting monoBehaviour. For this test - search by tag. 
        private List<IHorizontalMovable> _horizontalMovablePlayers = new List<IHorizontalMovable>();
        private List<IVerticalMovable> _verticalMovablePlayers = new List<IVerticalMovable>();

        private PlayerInput _playerInput;
        private InputAction _movement;
        private InputAction _jump;
        private bool _isMovementHeld;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movement = _playerInput.actions["Movement"];
            _jump = _playerInput.actions["Jump"];
            var playersByTag = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in playersByTag)
            {
                var horMovableComponent = player.GetComponent<IHorizontalMovable>();
                if (horMovableComponent != null)
                {
                    _horizontalMovablePlayers.Add(horMovableComponent);
                }

                var verMovableComponent = player.GetComponent<IVerticalMovable>();
                if (verMovableComponent != null)
                {
                    _verticalMovablePlayers.Add(verMovableComponent);
                }
            }
        }

        private void OnEnable()
        {
            _movement.started += MovementOnStarted;
            _movement.canceled += MovementOnCanceled;
            _jump.started += JumpOnStarted;
        }
        
        private void OnDisable()
        {
            _movement.started -= MovementOnStarted;
            _movement.canceled -= MovementOnCanceled;
            _isMovementHeld = false;
            StopCoroutine(Movement());
        }

        private void MovementOnCanceled(InputAction.CallbackContext obj)
        {
            _isMovementHeld = false;
        }

        private void MovementOnStarted(InputAction.CallbackContext obj)
        {
            _isMovementHeld = true;
            StartCoroutine(Movement());
        }

        private IEnumerator Movement()
        {
            while (_isMovementHeld)
            {
                var direction = _movement.ReadValue<Vector2>();
                foreach (var movable in _horizontalMovablePlayers)
                {
                    movable.Move(direction);
                }

                yield return null;
            }
        }
        
        private void JumpOnStarted(InputAction.CallbackContext obj)
        {
            foreach (var movable in _verticalMovablePlayers)
            {
                movable.VerticalMove(1);
            }
        }
    }
}