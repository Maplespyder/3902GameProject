using MarioClone.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    public class MenuOptionUpCommand : AbstractCommand<AbstractMenu>
    {
        public MenuOptionUpCommand(AbstractMenu menu) : base(menu) { }

        public override void InvokeCommand()
        {
            Receiver.MenuOptionUp();
        }
    }
}
