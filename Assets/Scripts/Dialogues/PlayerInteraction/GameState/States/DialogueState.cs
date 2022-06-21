using UnityEngine;

namespace Dialogues.PlayerInteraction.GameState.States
{
    public class DialogueState : State
    {
        public DialogueState(GameBehaviour gameBehaviour, Canvas canvas) : base(gameBehaviour, canvas)
        {
        }

        public override void Enter()
        {
            base.Enter();
            // May be create pause system and pause everything on dialogue enable?
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}