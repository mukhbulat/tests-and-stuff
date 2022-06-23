using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyMan
{
    [RequireComponent(typeof(PlayerInput))]
    [AddComponentMenu("SkyMan/Player")]
    public class Player : MonoBehaviour
    {
        public IPositionInterpreter PositionInterpreter { get; } = new PositionInterpreter();
        
        private int _targetLayer = 1 << 6;
        private PlayerInput _playerInput;
        private InputAction _click;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _playerInput = GetComponent<PlayerInput>();
            _click = _playerInput.actions["Click"];
        }

        private void OnEnable()
        {
            _click.canceled += ClickOnCanceled;
        }

        private void OnDisable()
        {
            _click.canceled -= ClickOnCanceled;
        }

        private void ClickOnCanceled(InputAction.CallbackContext obj)
        {
            var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var raycastHit, _targetLayer))
            {
                Debug.Log($"{raycastHit.point} point");
                PositionInterpreter.AddNewPosition(raycastHit.point);
            }
        }
    }
}