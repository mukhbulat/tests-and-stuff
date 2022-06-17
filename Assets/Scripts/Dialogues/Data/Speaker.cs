using UnityEngine;

namespace Dialogues.Data
{
    [CreateAssetMenu(menuName = "Dialogues/Speaker")]
    public class Speaker : ScriptableObject
    {
        [SerializeField] private string speakerName;
        [SerializeField] private Sprite sprite;

        public string Name => speakerName;
        public Sprite Sprite => sprite;
    }
}