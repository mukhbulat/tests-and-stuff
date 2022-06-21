using System;
using Dialogues.PlayerInteraction.GameState.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dialogues.PlayerInteraction.GameState
{
    // I know this is bad naming, but how to call this
    public class GameBehaviour : MonoBehaviour
    {
        public event Action NextLineRequested;
        
        [SerializeField] private Canvas dialogueCanvas;
        [SerializeField] private Canvas standardCanvas;
        
        private StateMachine _stateMachine;
        private State _dialogueState, _standardState;
        private Camera _mainCamera;
        

        private void Awake()
        {
            _mainCamera = Camera.main;

            _stateMachine = new StateMachine();

            _dialogueState = new DialogueState(this, dialogueCanvas);
            _standardState = new StandardState(this, standardCanvas);
            
            _stateMachine.Initialize(_standardState);
        }

        public void EnableDialogue(bool isEnabled)
        {
            _stateMachine.ChangeState(isEnabled ? _dialogueState : _standardState);
        }

        public void Clicked()
        {
            _stateMachine.CurrentState.ActionOnClick();
        }
        
        public void TryInteract()
        {
            var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                interactable?.Interact();
            }
        }

        public void NextLine()
        {
            NextLineRequested?.Invoke();
        }
    }
}