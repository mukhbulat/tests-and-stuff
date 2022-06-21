using System;
using Dialogues.PlayerInteraction.GameState;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dialogues.PlayerInteraction
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private GameBehaviour gameBehaviour;

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
            gameBehaviour.Clicked();
        }

    }
}