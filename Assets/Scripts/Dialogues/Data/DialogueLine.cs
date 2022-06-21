using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Data
{
    [CreateAssetMenu(menuName = "Dialogues/DialogueLine")]
    public class DialogueLine : ScriptableObject
    {
        [SerializeField] private string line;
        [SerializeField] private Speaker speaker;
        [SerializeField] private List<DialogueLine> choices;
        [SerializeField] private List<string> choicesLines;

        public string Line => line;
        public Speaker Speaker => speaker;
        public Dictionary<DialogueLine, string> Choices
        {
            get
            {
                if (choices == null || choices.Count == 0) return new Dictionary<DialogueLine, string>(0);
                var dict = new Dictionary<DialogueLine, string>(choices.Count);
                if (choices.Count == 1)
                {
                    dict.Add(choices[0], string.Empty);
                    return dict;
                }
                for (int i = 0; i < choices.Count; i++)
                {
                    dict.Add(choices[i], choicesLines[i]);
                }

                return dict;
            }
        }
    }
}