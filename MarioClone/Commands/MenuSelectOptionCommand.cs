using MarioClone.Menu;

namespace MarioClone.Commands
{
    public class MenuSelectOptionCommand : AbstractCommand<AbstractMenu>
    {
        public MenuSelectOptionCommand(AbstractMenu receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.MenuOptionSelect();
        }
    }
}