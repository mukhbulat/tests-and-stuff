using UnityEngine;

namespace Plarium.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        public Team Affinity => affinity;

        [SerializeField] private Team affinity;
    }
}