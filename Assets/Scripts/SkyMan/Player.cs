using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyMan
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _click;
        private Camera _mainCamera;
        public IInterpreter Interpreter { get; } = new Interpreter();

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
            Debug.Log(_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
            Interpreter.AddNewPosition(_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }
    }
}