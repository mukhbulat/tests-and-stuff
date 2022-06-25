using Plarium.SelectionSystem;
using UnityEngine;

namespace Plarium.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        public Team Affinity => affinity;
        public SelectionController SelectionController { get; private set; }

        [SerializeField] private Team affinity;

        private void OnEnable()
        {
            SelectionController = new SelectionController(affinity);
        }
    }
}