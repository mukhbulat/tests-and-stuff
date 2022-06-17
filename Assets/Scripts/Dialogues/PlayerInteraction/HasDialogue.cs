using System;
using System.Collections;
using Dialogues.Controllers;
using UnityEngine;

namespace Dialogues.PlayerInteraction
{
    public class HasDialogue : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueExplorer dialogueExplorer;

        private IEnumerator Start()
        {
            yield return null;
            dialogueExplorer = new DialogueExplorer();
            dialogueExplorer.LoadInstance();
            Debug.Log("Dialogue explorer created");
            Debug.Log(dialogueExplorer.GetFirstLine().Line);
        }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }

        private void StartDialogue()
        {
            
        }
    }
}