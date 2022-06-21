using System;
using Dialogues.PlayerInteraction.GameState.States;
using UnityEngine;

namespace Dialogues.PlayerInteraction.GameState
{
    // I know this is bad naming, but how to call this
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private Canvas dialogueCanvas;
        [SerializeField] private Canvas standardCanvas;
        
        private StateMachine _stateMachine;
        private State _dialogueState, _standardState;
        

        private void Awake()
        {
            _stateMachine = new StateMachine();

            _dialogueState = new DialogueState(this, dialogueCanvas);
            _standardState = new StandardState(this, standardCanvas);
            
            _stateMachine.Initialize(_standardState);
        }

        public void EnableDialogue(bool isEnabled)
        {
            _stateMachine.ChangeState(isEnabled ? _dialogueState : _standardState);
        }
    }
}