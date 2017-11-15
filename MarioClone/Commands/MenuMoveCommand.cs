using MarioClone.GameObjects;
using MarioClone.GameOver;

namespace MarioClone.Commands
{
    public class MenuMoveCommand : AbstractCommand<GameOverScreen>
    {
        public MenuMoveCommand(GameOverScreen receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.State == GameState.GameOver)
            {
                Receiver.MenuMoveCommand();
            }
        }
    }
}
