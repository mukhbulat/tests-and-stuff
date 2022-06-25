using Plarium.Player;
using UnityEngine;

namespace Plarium.SelectionSystem
{
    public class Selectable : MonoBehaviour, ISelectable
    {
        public Team Affinity => affinity;
        public SelectableData SelectableData => selectableData;
        
        [SerializeField] private Team affinity;
        [SerializeField] private SelectableData selectableData;
    }
}