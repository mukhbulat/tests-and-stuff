using System.Collections.Generic;
using UnityEngine;

namespace Plarium.SelectionSystem
{
    [CreateAssetMenu(menuName = "Plarium/SelectableType")]
    public class SelectableType : ScriptableObject
    {
        public string TypeName;
        public int TypePriority;
        // Selectables of this type sorted by priority from the best to worst.
        public List<SelectableData> SelectablesByPriority;
    }
}