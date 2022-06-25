using System.Collections.Generic;
using UnityEngine;

namespace Plarium.Player
{
    [CreateAssetMenu(menuName = "Plarium/Team")]
    public class Team : ScriptableObject
    {
        public string TeamName;
        public Material TeamMaterial;
        public List<Team> Allies;
    }
}