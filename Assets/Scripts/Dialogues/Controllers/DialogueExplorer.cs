using System;
using Dialogues.Data;
using UnityEngine;

namespace Dialogues.Controllers
{
    [Serializable]
    public class DialogueExplorer
    {
        [SerializeField] private DialogueLine startingLine;

        private DialogueLine _currentLine;
        public void LoadInstance()
        {
            _currentLine = startingLine;
            Debug.Log("Instance loaded");
            if (startingLine == null)
            {
                Debug.Log("starting line is null");
            }

            if (_currentLine == null)
            {
                Debug.Log("Current line is null");
            }
        }

        public DialogueLine GetDialogueLine()
        {
            return _currentLine;
        }

        public DialogueLine GetFirstLine()
        {
            return startingLine;
        }
    }
}