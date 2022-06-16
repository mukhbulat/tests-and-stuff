using UnityEngine;

namespace Dialogues.Data
{
    [CreateAssetMenu(menuName = "Dialogues/Speaker")]
    public class Speaker : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite sprite;

        public string Name => name;
        public Sprite Sprite => sprite;
    }
}