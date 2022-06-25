using System.Collections.Generic;

namespace Plarium.SelectionSystem
{
    public class SelectionController
    {
        public List<ISelectable> SelectedCharacters { get; private set; }

        public Dictionary<int, List<ISelectable>> Groups { get; private set; }

        private Team _playerAffinity;

        public SelectionController(Team playerAffinity)
        {
            _playerAffinity = playerAffinity;
            SelectedCharacters = new List<ISelectable>();
            Groups = new Dictionary<int, List<ISelectable>>(6);
        }
        
        public void RewriteGroup(int groupNum)
        {
            if (Groups.ContainsKey(groupNum))
            {
                Groups[groupNum] = SelectedCharacters;
            }
            else
            {
                Groups.Add(groupNum, SelectedCharacters);
            }
        }

        public void AddCharactersToGroup(int groupNum)
        {
            // todo check if characters are under player's management and if they match and add to group.
        }

        public void SelectGroup(int groupNum)
        {
            if (!Groups.ContainsKey(groupNum)) return;
            SelectedCharacters = Groups[groupNum];
        }

        public void AddCharactersToSelected(List<ISelectable> characters, bool isMultipleSelection)
        {
            if (characters.Count == 0)
            {
                if (isMultipleSelection)
                {
                    return;
                }
                else
                {
                    SelectedCharacters.Clear();
                }
            }
            var sortedCharacters = CheckCharactersByType(characters);
            if (isMultipleSelection)
            {
                if (SelectedCharacters.Count == 0)
                {
                    SelectedCharacters.AddRange(sortedCharacters);
                    return;
                }

                if (sortedCharacters[0].SelectableData.Type.TypePriority <
                    SelectedCharacters[0].SelectableData.Type.TypePriority)
                {
                    SelectedCharacters.Clear();
                    SelectedCharacters.AddRange(sortedCharacters);
                }
                else if (sortedCharacters[0].SelectableData.Type.TypePriority >
                         SelectedCharacters[0].SelectableData.Type.TypePriority)
                {
                    return;
                }
                else
                {
                    SelectedCharacters.AddRange(sortedCharacters);
                }
            }
            else
            {
                SelectedCharacters.Clear();
                SelectedCharacters.AddRange(sortedCharacters);
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