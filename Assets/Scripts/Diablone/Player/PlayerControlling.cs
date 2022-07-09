using System;
using Diablone.MovementSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Diablone.Player
{
    public class PlayerControlling : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        private GameObject _playerCharacter;
        private IMovable _playerMovable;
        private Camera _mainCamera;
        private int _terrainLayerMask = 1 << 7;

        private InputAction _move;
        private void Awake()
        {
            _mainCamera = Camera.main;
            _playerCharacter = GameObject.FindGameObjectWithTag("Player");
            _playerMovable = _playerCharacter.GetComponentInChildren<IMovable>();
            if (_playerCharacter == null)
            {
                Debug.Log("Player Character is not found.");
            }
            
            _move = _playerInput.actions["Move"];
        }

        private void OnEnable()
        {
            _move.canceled += MoveOnCanceled;
        }

        private void OnDisable()
        {
            _move.canceled -= MoveOnCanceled;
        }

        private void MoveOnCanceled(InputAction.CallbackContext obj)
        {
            var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _terrainLayerMask))
            {
                _playerMovable.Move(hit.point);
            }
        }
        
    }
}