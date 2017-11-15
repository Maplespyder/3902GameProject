using MarioClone.Menu;

namespace MarioClone.Commands
{
    public class MenuMoveCommand : AbstractCommand<MenuScreen>
    {
        public MenuMoveCommand(MenuScreen receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.state == GameState.GameOver || MarioCloneGame.state == GameState.Win)
            {
                 Receiver.MenuMoveCommand();
            }
        }
    }
}
