using UnityEngine;

namespace Plarium.SelectionSystem
{
    [CreateAssetMenu(menuName = "Plarium/Selectable Data")]
    public class SelectableData : ScriptableObject
    {
        public string SelectableName;
        public SelectableType Type;
        public Sprite Sprite;
    }
}