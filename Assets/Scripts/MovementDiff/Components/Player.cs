using System;
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
        private List<IMovable> _movables = new List<IMovable>();

        private PlayerInput _playerInput;
        private InputAction _movement;
        private bool _isMovementHeld;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movement = _playerInput.actions["Movement"];
            var movablesByTag = GameObject.FindGameObjectsWithTag("Player");
            foreach (var movableObject in movablesByTag)
            {
                var movableComponent = movableObject.GetComponent<IMovable>();
                if (movableComponent != null)
                {
                    _movables.Add(movableComponent);
                }
            }
        }

        private void OnEnable()
        {
            _movement.started += MovementOnStarted;
            _movement.canceled += MovementOnCanceled;
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
                foreach (var movable in _movables)
                {
                    movable.Move(direction);
                }

                yield return null;
            }
        }
    }
}