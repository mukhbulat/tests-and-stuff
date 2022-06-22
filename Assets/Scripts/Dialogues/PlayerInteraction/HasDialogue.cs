using System;
using Dialogues.Data;
using UnityEngine;

namespace Dialogues.PlayerInteraction
{
    public class HasDialogue : MonoBehaviour, IInteractable
    {
        public event Action<bool, HasDialogue> DialogueEnabled;
        public event Action LineChanged;
        
        [SerializeField] private DialogueLine startingLine;
        [SerializeField] private bool isUnique = false;
        public DialogueLine CurrentLine { get; private set; }
        public bool IsUnique => isUnique;
        
        public void Interact()
        {
            CurrentLine = startingLine;
            StartDialogue();
        }

        private void StartDialogue()
        {
            DialogueEnabled?.Invoke(true, this);
            LineChanged?.Invoke();
        }
        public void ChangeLine(DialogueLine choice)
        {
            if (choice == null)
            {
                DialogueEnabled?.Invoke(false, this);
                return;
            }
            CurrentLine = choice;
            LineChanged?.Invoke();
        }
    }
}