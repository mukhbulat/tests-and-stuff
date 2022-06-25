using System.Collections.Generic;

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

        private List<ISelectable> CheckCharactersByType(List<ISelectable> characters)
        {
            if (characters.Count == 0) return null;
            var charactersOfOneType = new List<ISelectable>();
            var bestType = characters[0].SelectableData.Type.TypePriority;
            foreach (var selectable in characters)
            {
                if (selectable.SelectableData.Type.TypePriority < bestType)
                {
                    bestType = selectable.SelectableData.Type.TypePriority;
                }
            }

            foreach (var selectable in characters)
            {
                if (selectable.SelectableData.Type.TypePriority == bestType)
                {
                    charactersOfOneType.Add(selectable);
                }
            }

            return charactersOfOneType;
        }
    }
}