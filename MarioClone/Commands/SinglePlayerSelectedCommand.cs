using MarioClone.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.Commands
{
    public class SinglePlayerSelectedCommand : AbstractCommand<MainMenu>
    {
        public SinglePlayerSelectedCommand(MainMenu receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            Receiver.SinglePlayerSelected();
        }
    }
}
