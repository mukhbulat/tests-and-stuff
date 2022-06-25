using System.Collections.Generic;
using UnityEngine;

namespace Plarium.SelectionSystem
{
    [CreateAssetMenu(menuName = "Plarium/Team")]
    public class Team : ScriptableObject
    {
        public string TeamName;
        public Material TeamMaterial;
        public List<Team> Allies;
    }
}