using MarioClone.Menu;

namespace MarioClone.Commands
{
    public class MenuSelectCommand : AbstractCommand<MenuScreen>
    {
        public MenuSelectCommand(MenuScreen receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            if (MarioCloneGame.State == GameState.GameOver || MarioCloneGame.State == GameState.Win)
            {
                Receiver.MenuSelectCommand();
            }
        }
    }
}