using MarioClone.GameObjects;
using MarioClone.GameOver;

namespace MarioClone.Commands
{
    public class MenuSelectCommand : AbstractCommand<GameOverScreen>
    {
        public MenuSelectCommand(GameOverScreen receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.state == GameState.GameOver)
            {
                Receiver.MenuSelectCommand();
            }
        }
    }
}