using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dialogues.PlayerInteraction
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        private Camera _mainCamera;
        private InputAction _click;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            _click = playerInput.actions["Click"];
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _click.canceled += ClickOnCanceled;
        }

        private void ClickOnCanceled(InputAction.CallbackContext obj)
        {
            TryInteract();
        }

        private void TryInteract()
        {
            var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                interactable?.Interact();
            }
        }
    }
}