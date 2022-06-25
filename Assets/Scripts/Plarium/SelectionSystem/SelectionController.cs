using System.Collections.Generic;
using Plarium.Player;

namespace Plarium.SelectionSystem
{
    public class SelectionController
    {
        public List<ISelectable> SelectedCharacters { get; private set; }

        private Team _playerAffinity;

        public SelectionController(Team playerAffinity)
        {
            _playerAffinity = playerAffinity;
        }

        public void AddCharactersToSelected(List<ISelectable> characters)
        {
            foreach (var selectable in characters)
            {
                if (selectable.Affinity == _playerAffinity)
                {
                    SelectedCharacters.Add(selectable);
                }
            }
        }
    }
}