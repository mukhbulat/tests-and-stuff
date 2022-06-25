using Plarium.Player;

namespace Plarium.SelectionSystem
{
    public interface ISelectable
    {
        public Team Affinity { get; }
        public SelectableData SelectableData { get; }
    }
}