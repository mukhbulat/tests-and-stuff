using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Data
{
    [Serializable]
    public class DialogueLine
    {
        [SerializeField] private string line;
        [SerializeField] private List<DialogueLine> choices;
        [SerializeField] private List<string> choicesLines;

        public string Line => line;
        public Dictionary<DialogueLine, string> Choices
        {
            get
            {
                if (choices == null || choices.Count == 0) return new Dictionary<DialogueLine, string>(0);
                var dict = new Dictionary<DialogueLine, string>(choices.Count);
                for (int i = 0; i < choices.Count; i++)
                {
                    dict.Add(choices[i], choicesLines[i]);
                }

                return dict;
            }
        }
    }
}