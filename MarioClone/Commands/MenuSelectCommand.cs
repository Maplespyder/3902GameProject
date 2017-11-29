using MarioClone.Menu;

namespace MarioClone.Commands
{
    public class MenuSelectCommand : AbstractCommand<MenuScreen>
    {
        public MenuSelectCommand(MenuScreen receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.MenuSelectCommand();
        }
    }
}