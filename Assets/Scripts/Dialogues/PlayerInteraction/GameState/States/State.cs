using UnityEngine;

namespace Dialogues.PlayerInteraction.GameState.States
{
    public abstract class State
    {
        protected GameBehaviour GameBehaviour;
        protected Canvas CurrentCanvas; 

        protected State(GameBehaviour gameBehaviour, Canvas canvas)
        {
            GameBehaviour = gameBehaviour;
            CurrentCanvas = canvas;
        }

        public virtual void Enter()
        {
            CurrentCanvas.enabled = true;
        }

        public virtual void Exit()
        {
            CurrentCanvas.enabled = false;
        }
    }
}