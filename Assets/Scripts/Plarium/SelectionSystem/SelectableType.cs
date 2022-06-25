using System.Collections.Generic;
using UnityEngine;

namespace Plarium.SelectionSystem
{
    [CreateAssetMenu(menuName = "Plarium/SelectableType")]
    public class SelectableType : ScriptableObject
    {
        public string TypeName;
        public List<SelectableData> SelectablesPriority;
    }
}